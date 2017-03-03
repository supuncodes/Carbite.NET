using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System.Reflection;

namespace Carbite
{
    internal static class RequestProcessor
    {

        internal static PathRepo repo;

        internal static void InitRepo(Assembly callingAssembly) 
        {
            repo = new PathRepo();

            var referencedAssemblies = callingAssembly.GetReferencedAssemblies();
            var currentAsm = Assembly.GetExecutingAssembly();
            foreach (var refAsm in referencedAssemblies)
            {
                try
                {
                    var asm = Assembly.LoadWithPartialName(refAsm.FullName);
                    if (asm == currentAsm)
                        continue;

                    foreach (var typ in asm.GetTypes()) 
                    {
                        if (typ.IsSubclassOf(typeof(RestService))) 
                        {
                            var classAttribObj = typ.GetCustomAttributes(typeof(RestClass), false).FirstOrDefault();
                            string baseUrl;
                            if (classAttribObj != null)
                            {
                                var classAttrib = (RestClass)classAttribObj;
                                baseUrl = classAttrib.Path;
                            }
                            else baseUrl = "";
                               
                            var methods = typ.GetMethods();


                            foreach (var method in methods) 
                            {
                                var methodAttribObject = method.GetCustomAttributes(typeof(RestMethod), false).FirstOrDefault();

                                if (methodAttribObject != null) 
                                {
                                    var methodAttrib = (RestMethod)methodAttribObject;
                                    var finalUrl = baseUrl + (methodAttrib.Path == null ? "" : methodAttrib.Path);
                                    var finalMethod = methodAttrib.Method;

                                    PathExecutionInfo exeInfo = new PathExecutionInfo();
                                    exeInfo.Type = typ;
                                    exeInfo.Method = method;
                                    repo.AddExecutionInfo (finalMethod, finalUrl, exeInfo);
                                }
                            }
                        }

                    }
                }
                catch (Exception ex) 
                {
                }
            }
        }

        internal static void Process(HttpContext context)
        {
            FrameworkResponse frmRes = new FrameworkResponse();

            HttpRequest req = context.Request;
            HttpResponse res = context.Response;
            
            var relUrl = req.Url.ToString().Replace("http://" + req.Url.Authority, "");

            PathExecutionParams exeParams = repo[req.HttpMethod, relUrl];

            try
            {
                if (exeParams != null)
                {
                    object newObj = Activator.CreateInstance(exeParams.ExecutionInfo.Type);
                    var exeMethod = exeParams.ExecutionInfo.Method;
                    List<object> activatorParams = new List<object>();
                    var methodParams = exeMethod.GetParameters();
                    
                    foreach (var mParam in methodParams)
                    {
                        if (exeParams.Parameters.ContainsKey(mParam.Name))
                        {
                            var strValue = exeParams.Parameters[mParam.Name];
                            object convertedValue = Convert.ChangeType(strValue,mParam.ParameterType);
                            activatorParams.Add(convertedValue);
                        }
                        else 
                        {
                            throw new ParameterMismatchException();  
                        }
                    }

                    object output = exeMethod.Invoke(newObj, activatorParams.ToArray());
                    frmRes.Success = true;
                    frmRes.Result = output;
                    res.ContentType = "application/json";
                }
                else
                {
                    res.ContentType = "application/json";
                    frmRes.Success = false;
                    frmRes.Result = "404 Not Found";
                    res.StatusCode = 404;
                }
            }
            catch (Exception ex) 
            {
                res.ContentType = "application/json";
                frmRes.Success = false;
                frmRes.Result = ex;
                res.StatusCode = 500;
            }

            res.Write(getJson(frmRes));
        }

        private static string getJson(FrameworkResponse response)
        {
            return JsonConvert.SerializeObject(response);            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Carbite
{
    internal class PathRepo
    {

        private Dictionary<string, Dictionary<string, PathExecutionInfo>> pathExecutionInfo;

        internal PathExecutionParams this[string method, string reqUrl]
        {
            get
            {
                return getExecutionParams(method, reqUrl);
            }
        }

        private PathExecutionParams getExecutionParams(string method, string reqUrl) 
        {
            PathExecutionParams executionParams = null;
            bool isFound = false;
            Dictionary<string, string> variables = null;
            PathExecutionInfo executionInfo = null;

            string[] urlSplit = reqUrl.Split('/');

            if (pathExecutionInfo.ContainsKey(method)) 
            {

                variables = new Dictionary<string, string>();

                foreach (KeyValuePair<string, PathExecutionInfo> onePath in pathExecutionInfo[method]) 
                {
                    string[] definedPathSplit = onePath.Key.Split('/');

                    if (definedPathSplit.Length == urlSplit.Length) 
                    {
                        variables.Clear();
                        isFound = true;

                        for (int i = 0; i < definedPathSplit.Length; i++) 
                        {
                            if (definedPathSplit[i].StartsWith("@"))
                                variables.Add(definedPathSplit[i].Substring(1), urlSplit[i]);
                            else 
                            {
                                if (definedPathSplit[i] != urlSplit[i]) 
                                {
                                    isFound = false;
                                    break;
                                }
                            }
                        } 
                        
                    }

                    if (isFound)
                    {
                        executionInfo = onePath.Value;
                        break;
                    }
                }
            }
                
            if (isFound)
            {
                executionParams = new PathExecutionParams();
                executionParams.ExecutionInfo = executionInfo;
                executionParams.Parameters = variables;
            }

            return executionParams;
        }



        internal void AddExecutionInfo(string method, string reqUrl, PathExecutionInfo value) 
        {
            Dictionary<string, PathExecutionInfo> methodDic;

            if (!pathExecutionInfo.ContainsKey(method))
            {
                methodDic = new Dictionary<string, PathExecutionInfo>();
                pathExecutionInfo.Add(method, methodDic);
            }
            else methodDic = pathExecutionInfo[method];

            if (!methodDic.ContainsKey(reqUrl))
                methodDic.Add(reqUrl, value);
        }

        internal PathRepo() 
        {
            this.pathExecutionInfo = new Dictionary<string, Dictionary<string, PathExecutionInfo>>();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Carbite
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class RestMethod : Attribute
    {
        private string method;
        public string Method
        {
            get { return method; }
            set { method = value; }
        }

        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public RestMethod(string method, string path) 
        {
            this.method = method;
            this.path = path;
        }

        public RestMethod(string method) 
        {
            this.method = method;
        }
    }
}

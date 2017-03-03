using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Carbite
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class RestClass : Attribute
    {
        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public RestClass(string path) 
        {
            this.path = path;
        }
    }
}

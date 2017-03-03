using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Carbite
{
    public class RestService
    {
        private HttpContext context;
        public HttpContext Context
        {
            get { return context; }
            internal set { context = value; }
        }

    }
}

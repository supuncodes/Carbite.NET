using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Carbite
{
    public class RequestDispatcher : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            RequestProcessor.Process(context);
        }

    }
}

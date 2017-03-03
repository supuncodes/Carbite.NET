using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Carbite
{
    public class FrameworkResponse
    {
        private bool success;
        public bool Success
        {
            get { return success; }
            internal set { success = value; }
        }
        
        private object result;
        public object Result
        {
            get { return result; }
            internal set { result = value; }
        }

    }
}

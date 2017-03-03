using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Carbite
{
    public abstract class RestService <T> : RestService
    {
        [RestMethod("GET", "/@skip/@take")]
        public abstract T GetAll(int skip, int take);

        [RestMethod("POST")]
        public abstract void Insert();

        [RestMethod("PUT")]
        public abstract void Update();

        [RestMethod("DELETE")]
        public abstract void Delete();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


namespace Carbite
{
    public static class FrameworkInitializer
    {
        public static void Initialize() 
        {
           RequestProcessor.InitRepo(Assembly.GetCallingAssembly());
        }

    }
}

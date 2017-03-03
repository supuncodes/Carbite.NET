using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleRest
{

    public class Customer
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        private int age;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

    }
}

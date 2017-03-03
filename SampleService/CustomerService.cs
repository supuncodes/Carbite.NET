using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Carbite;


namespace SampleRest
{
    [RestClass ("/customers")]
    public class CustomerService: RestService
    {

        [RestMethod("GET", "/@skip/@take")]
        public string ShowParameters(int skip, int take)
        {
            return "SKIP = " + skip + " , TAKE = " + take;
        }
        
        [RestMethod("GET")]
        public Customer ShowDummyCustomer()
        {
            return new Customer() { Age = 12, Name = "Test" };
        }
    }
}

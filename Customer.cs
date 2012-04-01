using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBilling
{
    public class Customer
    {
        public string ID { get; set; }
        public bool IsAffiliate { get; set; }
        public bool IsEmployee { get; set; }
        public DateTime JoinedOn { get; set; }
    }
}

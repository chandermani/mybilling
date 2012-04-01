using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBilling
{
    public class Product
    {
        public string ID { get; set; }
        public float Price { get; set; }
        public ProductType Type { get; set; }
    }

    public enum ProductType
    {
        Others=0,
        Grocery=1,
    }
}

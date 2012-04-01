using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBilling
{
    public class ProductLineItem
    {
        public Product Product { get; set; }
        public float Quantity { get; set; }
        public float GrossPrice
        {
            get
            {
                return Product.Price * Quantity;
            }
        }
        public ProductType ProductType
        {
            get { return this.Product.Type; }
        }
    }

    public class DiscountLineItem
    {
        public float DiscountAmount { get; private set; }
        public DiscountLineItem(float discountAmount)
        {
            DiscountAmount = discountAmount;
        }        
    }
}

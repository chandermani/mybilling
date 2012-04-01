using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBilling
{
    public class Bill
    {
        private DiscountPipeline _discountPipeLine;
        private List<ProductLineItem> _productLineItems;
        private List<DiscountLineItem> _discountLineItems;
        private bool _requireDiscountPipelineReRun;

        public Customer Customer { get; private set; }

        public float GrossBillAmount
        {
            get
            {
                return _productLineItems.Sum(i => i.GrossPrice);
            }
        }
        public float TotalDiscount
        {
            get
            {
                if (_requireDiscountPipelineReRun) ExecuteDiscountPipeLine();
                return _discountLineItems.Sum(d => d.DiscountAmount);
            }
        }

        private void ExecuteDiscountPipeLine()
        {
            _discountLineItems.Clear();
            _discountLineItems.AddRange(_discountPipeLine.Execute());
            _requireDiscountPipelineReRun = false;
        }
        public float NetBillAmount
        {
            get
            {
                return GrossBillAmount - TotalDiscount;
            }
        }

        public Bill(Customer forCustomer)
        {
            _productLineItems = new List<ProductLineItem>();
            _discountLineItems = new List<DiscountLineItem>();
            Customer = forCustomer;
            _discountPipeLine = new DiscountPipeline(this, forCustomer);
        }

        public void Add(ProductLineItem productLineItem)
        {
            _productLineItems.Add(productLineItem);
            _requireDiscountPipelineReRun = true;
        }

        public void Add(DiscountLineItem discountLineItem)
        {
            _discountLineItems.Add(discountLineItem);
        }

        public List<ProductLineItem> GetProductItems()
        {
            return new List<ProductLineItem>(_productLineItems);
        }

        private void ClearExistingDiscounts()
        {
            _discountLineItems.Clear();
        }
        
        
    }
}

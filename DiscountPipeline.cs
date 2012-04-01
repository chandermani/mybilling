using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBilling
{
    public class DiscountPipeline
    {
        private List<IDiscounter> _percentageDiscounters;
        public Bill Bill { get; private set; }
        public Customer Customer { get; private set; }
        public DiscountPipeline(Bill bill, Customer customer)
        {
            _percentageDiscounters = new List<IDiscounter>();
            this.Bill = bill;
            this.Customer = customer;
            LoadPercentageDiscounters();
        }

        public List<DiscountLineItem> Execute()
        {
            List<DiscountLineItem> applicableDiscounts = new List<DiscountLineItem>();
            //We would use the maximum discount strategy for percentage discounters
            float maxDiscount = _percentageDiscounters.Max(d => d.GetDiscount());
            if (maxDiscount > 0)
                applicableDiscounts.Add(new DiscountLineItem(maxDiscount));

            //Add the discount applicable on the total bill
            float totalBillDiscount = new BilledTotalDiscounter(this.Bill, this.Customer, applicableDiscounts).GetDiscount();
            if (totalBillDiscount > 0)
                applicableDiscounts.Add(new DiscountLineItem(totalBillDiscount));
            return applicableDiscounts;
        }
        private void LoadPercentageDiscounters()
        {
            _percentageDiscounters.Add(new EmployeeDiscounter(this.Bill, this.Customer));
            _percentageDiscounters.Add(new AffiliateDiscounter(this.Bill, this.Customer));
            _percentageDiscounters.Add(new LoyaltyDiscounter(this.Bill, this.Customer));
        }
    }
}

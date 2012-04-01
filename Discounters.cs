using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBilling
{
    public interface IDiscounter
    {
        bool CanApply { get; }
        float GetDiscount();
    }
    public abstract class Discounter:IDiscounter
    {
        public Bill Bill { get; private set; }
        public Customer Customer { get; private set; }
        public Discounter(Bill bill, Customer customer)
        {
            this.Bill = bill;
            this.Customer = customer;
        }

        public abstract bool CanApply { get; }
        public abstract float GetDiscount();
        
    }

    public class EmployeeDiscounter : Discounter
    {
        public EmployeeDiscounter(Bill bill, Customer customer)
            : base(bill, customer)
        {

        }

        public override bool CanApply
        {
            get { return this.Customer.IsEmployee; }
        }

        public override float GetDiscount()
        {
            if (!CanApply) return 0.0f;
            return this.Bill.GetProductItems()
                                .Where(item => ((ProductLineItem)item).ProductType != ProductType.Grocery)
                                .Sum(item => item.GrossPrice) * 0.3f;
        }
    }

    public class AffiliateDiscounter : Discounter
    {
        public AffiliateDiscounter(Bill bill, Customer customer)
            : base(bill, customer)
        {

        }

        public override bool CanApply
        {
            get { return this.Customer.IsAffiliate; }
        }

        public override float GetDiscount()
        {
            if (!CanApply) return 0.0f;
            return this.Bill.GetProductItems()
                                .Where(item => ((ProductLineItem)item).ProductType != ProductType.Grocery)
                                .Sum(item => item.GrossPrice) * 0.1f;
        }
    }

    public class LoyaltyDiscounter : Discounter
    {
        public LoyaltyDiscounter(Bill bill, Customer customer)
            : base(bill, customer)
        {

        }

        public override bool CanApply
        {
            get { return this.Customer.JoinedOn.AddYears(2) <= DateTime.Now; }
        }

        public override float GetDiscount()
        {
            if (!CanApply) return 0.0f;
            return this.Bill.GetProductItems()
                                .Where(item => ((ProductLineItem)item).ProductType != ProductType.Grocery)
                                .Sum(item => item.GrossPrice) * 0.05f;
        }
    }

    public class BilledTotalDiscounter : Discounter
    {
        private List<DiscountLineItem> _existingDiscounts;
        public BilledTotalDiscounter(Bill bill, Customer customer,List<DiscountLineItem> existingDiscounts)
            : base(bill, customer)
        {
            _existingDiscounts = existingDiscounts;
        }
        public override bool CanApply
        {
            get { return true; }
        }
        public override float GetDiscount()
        {
            if (!CanApply) return 0.0f;
            return (int)((this.Bill.GrossBillAmount - _existingDiscounts.Sum(d => d.DiscountAmount)) / 100) * 5;
        }
    }
}

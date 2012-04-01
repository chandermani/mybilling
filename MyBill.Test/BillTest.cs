using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBilling;

namespace MyBill.Test
{
    [TestClass]
    public class BillTest
    {
        [TestMethod]
        public void Test_Empty_Bill()
        {
            Bill bill = new Bill(new Customer() { });
            Assert.IsTrue(bill.GrossBillAmount == 0.0f,"Gross bill amount missmatch");
            Assert.IsTrue(bill.TotalDiscount == 0.0f,"Total discount amount missmatch");
            Assert.IsTrue(bill.NetBillAmount == 0.0f, "Net amount missmatch");
        }
        [TestMethod]
        public void Test_Bill_With_AmountLessThan100_Not_Special_Customer()
        {
            Bill bill = new Bill(new Customer() { JoinedOn = DateTime.Now });
            bill.Add(
                new ProductLineItem()
                                    {
                                        Product = new Product() { ID = "1", Price = 20.5f },
                                        Quantity = 2
                                    }
                    );

            bill.Add(
                new ProductLineItem()
                                 {
                                     Product = new Product() { ID = "1", Price = 11.2f },
                                     Quantity = 1
                                 }

                 );
            Assert.AreEqual<float>(52.2f,bill.GrossBillAmount, "Gross bill amount missmatch");
            Assert.AreEqual<float>(0.0f,bill.TotalDiscount, "Total discount amount missmatch");
            Assert.AreEqual<float>(52.2f,bill.NetBillAmount, "Net amount missmatch");
        }
        [TestMethod]
        public void Test_Bill_With_AmountLessThan100_Employee_Special_Customer()
        {
            Bill bill = new Bill(new Customer() { JoinedOn = DateTime.Now, IsEmployee = true });
            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 20.5f },
                    Quantity = 2
                }
                    );

            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 11.2f },
                    Quantity = 1
                }

                 );
            Assert.AreEqual(52.2f,bill.GrossBillAmount, 0.01f,"Gross bill amount missmatch");
            Assert.AreEqual(15.66f, bill.TotalDiscount, 0.01f, "Total discount amount missmatch");
            Assert.AreEqual(36.54f, bill.NetBillAmount, 0.01f, "Net amount missmatch");
        }
        [TestMethod]
        public void Test_Bill_With_AmountLessThan100_Affiliate_Customer()
        {
            Bill bill = new Bill(new Customer() { JoinedOn = DateTime.Now, IsAffiliate=true });
            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 20.5f },
                    Quantity = 2
                }
                    );

            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 11.2f },
                    Quantity = 1
                }

                 );
            Assert.AreEqual(52.2f, bill.GrossBillAmount, 0.01f, "Gross bill amount missmatch");
            Assert.AreEqual(5.22f, bill.TotalDiscount, 0.01f, "Total discount amount missmatch");
            Assert.AreEqual(46.98f, bill.NetBillAmount, 0.01f, "Net amount missmatch");
        }
        [TestMethod]
        public void Test_Bill_With_AmountLessThan100_Loyalty_Customer()
        {
            Bill bill = new Bill(new Customer() { JoinedOn = DateTime.Now.AddYears(-2) });
            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 20.5f },
                    Quantity = 2
                }
                    );

            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 11.2f },
                    Quantity = 1
                }

                 );
            Assert.AreEqual(52.2f, bill.GrossBillAmount, 0.01f, "Gross bill amount missmatch");
            Assert.AreEqual(2.61f, bill.TotalDiscount, 0.01f, "Total discount amount missmatch");
            Assert.AreEqual(49.59f, bill.NetBillAmount, 0.01f, "Net amount missmatch");
        }
        [TestMethod]
        public void Test_Bill_With_AmountMoreThan100_Not_Special_Customer()
        {
            Bill bill = new Bill(new Customer() { JoinedOn = DateTime.Now });
            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 20.5f },
                    Quantity = 13
                }
                    );

            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 11.2f },
                    Quantity = 5
                }

                 );
            Assert.AreEqual<float>(322.5f, bill.GrossBillAmount, "Gross bill amount missmatch");
            Assert.AreEqual<float>(15.0f, bill.TotalDiscount, "Total discount amount missmatch");
            Assert.AreEqual<float>(307.5f, bill.NetBillAmount, "Net amount missmatch");
        }
        [TestMethod]
        public void Test_Bill_With_AmountMoreThan100_Employee_Customer()
        {
            Bill bill = new Bill(new Customer() { JoinedOn = DateTime.Now,IsEmployee=true });
            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 20.5f },
                    Quantity = 13
                }
                    );

            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 11.2f },
                    Quantity = 5
                }

                 );
            Assert.AreEqual(322.5f, bill.GrossBillAmount,0.01f, "Gross bill amount missmatch");
            Assert.AreEqual(106.75f, bill.TotalDiscount, 0.01f, "Total discount amount missmatch");
            Assert.AreEqual(215.75f, bill.NetBillAmount, 0.01f, "Net amount missmatch");
        }
        [TestMethod]
        public void Test_Bill_With_AmountMoreThan100_Affiliate_Customer()
        {
            Bill bill = new Bill(new Customer() { JoinedOn = DateTime.Now,IsAffiliate=true });
            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 20.5f },
                    Quantity = 13
                }
                    );

            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 11.2f },
                    Quantity = 5
                }

                 );
            Assert.AreEqual(322.5f, bill.GrossBillAmount, 0.01f, "Gross bill amount missmatch");
            Assert.AreEqual(42.25f, bill.TotalDiscount, 0.01f, "Total discount amount missmatch");
            Assert.AreEqual(280.25f, bill.NetBillAmount, 0.01f, "Net amount missmatch");
        }
        [TestMethod]
        public void Test_Bill_With_AmountMoreThan100_Loyalty_Customer()
        {
            Bill bill = new Bill(new Customer() { JoinedOn = DateTime.Now.AddDays(-900) });
            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 20.5f },
                    Quantity = 13
                }
                    );

            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 11.2f },
                    Quantity = 5
                }

                 );
            Assert.AreEqual(322.5f, bill.GrossBillAmount, 0.01f, "Gross bill amount missmatch");
            Assert.AreEqual(31.125f, bill.TotalDiscount, 0.01f, "Total discount amount missmatch");
            Assert.AreEqual(291.375f, bill.NetBillAmount, 0.01f, "Net amount missmatch");
        }
        [TestMethod]
        public void Test_Bill_With_Adding_Items_One_After_Another()
        {
            Bill bill = new Bill(new Customer() { JoinedOn = DateTime.Now.AddDays(-900) });
            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 20.5f },
                    Quantity = 13
                }
                    );

            Assert.AreEqual(266.5f, bill.GrossBillAmount, 0.01f, "Gross bill amount missmatch");
            Assert.AreEqual(23.325f, bill.TotalDiscount, 0.01f, "Total discount amount missmatch");
            Assert.AreEqual(243.175f, bill.NetBillAmount, 0.01f, "Net amount missmatch");

            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 11.1f },
                    Quantity = 7
                }

                 );
            Assert.AreEqual(344.2f, bill.GrossBillAmount, 0.01f, "Gross bill amount missmatch after adding product 2");
            Assert.AreEqual(32.21f, bill.TotalDiscount, 0.01f, "Total discount amount missmatch after adding product 2");
            Assert.AreEqual(311.99f, bill.NetBillAmount, 0.01f, "Net amount missmatch after adding product 2");

            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 30.0f },
                    Quantity = 24
                }

                 );
            Assert.AreEqual(1064.2f, bill.GrossBillAmount, 0.01f, "Gross bill amount missmatch after adding product 3");
            Assert.AreEqual(103.21f, bill.TotalDiscount, 0.01f, "Total discount amount missmatch after adding product 3");
            Assert.AreEqual(960.98f, bill.NetBillAmount, 0.01f, "Net amount missmatch after adding product 3");
        }
        [TestMethod]
        public void Test_Bill_With_Affiliate_Loyalty_Customer_Applies_Only_Affiliate_Discount()
        {
            Bill bill = new Bill(new Customer() { JoinedOn = DateTime.Now.AddDays(-900), IsAffiliate = true });
            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 20.5f },
                    Quantity = 13
                }
                    );

            bill.Add(
                new ProductLineItem()
                {
                    Product = new Product() { ID = "1", Price = 11.2f },
                    Quantity = 5
                }

                 );
            Assert.AreEqual(322.5f, bill.GrossBillAmount, 0.01f, "Gross bill amount missmatch");
            Assert.AreEqual(42.25f, bill.TotalDiscount, 0.01f, "Total discount amount missmatch");
            Assert.AreEqual(280.25f, bill.NetBillAmount, 0.01f, "Net amount missmatch");
        }
    }
}

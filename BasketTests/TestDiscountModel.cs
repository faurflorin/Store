using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingBasket.Model;
using ShoppingBasket.Interface;

namespace BasketTests
{
    [TestClass]
    public class TestDiscountModel
    {
        [TestMethod]
        public void TestCreateDiscount()
        {
            IDiscount discount = new ValueDiscount("12324", 3);

            Assert.AreEqual("12324", discount.ProductId);
            Assert.AreEqual(3, discount.Value);
            Assert.IsNull(discount.ValidFrom);
            Assert.IsNull(discount.ValidTo);
            Assert.IsTrue(discount.IsActive);
        }

        [TestMethod]
        public void TestCalculateValueDiscount()
        {
            Product product = new Product("Apples", 2, UnitType.bag);
            IDiscount discount = new ValueDiscount("prodId", 0.5m);

            string message = "";
            decimal discountValue = discount.CalculateDiscount(product, out message);

            Assert.AreEqual(0.5m, discountValue);
            Console.WriteLine(message);
            Assert.AreEqual("Apples 50p off: -50p", message);
        }

        [TestMethod]
        public void TestCalculatePercentageDiscount()
        {
            Product product = new Product("Apples", 2, UnitType.bag);
            IDiscount discount = new PercentageDiscount("prodId", 20);

            string message = "";
            decimal discountValue = discount.CalculateDiscount(product, out message);

            Assert.AreEqual(0.4m, discountValue);
            Console.WriteLine(message);
            Assert.AreEqual("Apples 20% off: -40p", message);
        }
    }
}

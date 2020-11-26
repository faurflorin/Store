using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingBasket.Model;

namespace BasketTests
{
    [TestClass]
    public class TestProductModel
    {
        [TestMethod]
        public void TestCreateProduct()
        {
            Product product = new Product("Milk", 20, UnitType.can);

            Assert.IsTrue(!String.IsNullOrEmpty(product.Id)); // test that an internal id was created
            Assert.AreEqual("Milk", product.Name);
        }

        [TestMethod]
        public void TestTrimNameCreateProduct()
        {
            Product product = new Product("  Milk  ", 20, UnitType.can);

            Assert.IsTrue(!String.IsNullOrEmpty(product.Id)); // test that an internal id was created
            Assert.AreEqual("Milk", product.Name);
        }

        /// <summary>
        /// can not create a product with empty or null name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestFailCreateProductWithEmptyName()
        {
            Product product = new Product("", 20, UnitType.can);
        }

        /// <summary>
        /// can not create a product with negative price
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestFailCreateProductWithNegativePrice()
        {
            Product product = new Product("Milk", -20, UnitType.can);
        }
    }
}

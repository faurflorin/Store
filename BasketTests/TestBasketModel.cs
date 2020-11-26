using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingBasket.Model;
using ShoppingBasket.Enums;

namespace BasketTests
{
    [TestClass]
    public class TestBasketModel
    {
        [TestMethod]
        public void TestCreateBasket()
        {
            Basket basket = new Basket();
            Assert.IsNotNull(basket);
        }

        [TestMethod]
        public void TestAddProductToBasket()
        {
            Basket basket = new Basket();
            basket.AddProduct(new Product("Milk", 3, UnitType.bottle));

            Assert.AreEqual(1, basket.Products.Count);
        }

        [TestMethod]
        public void TestRemoveProductToBasket()
        {
            Basket basket = new Basket();
            basket.AddProduct(new Product("Milk", 3, UnitType.bottle));
            basket.AddProduct(new Product("Beans", 3, UnitType.can));

            Assert.AreEqual(2, basket.Products.Count);

            basket.RemoveProductByName("milk ");
            Assert.AreEqual(1, basket.Products.Count);
        }
    }
}

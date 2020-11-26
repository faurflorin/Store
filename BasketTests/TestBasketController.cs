using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingBasket.Controllers;
using ShoppingBasket.Model;
using ShoppingBasket.Interface;

namespace BasketTests
{
    [TestClass]
    public class TestBasketController
    {
        [TestInitialize]
        public void testInit()
        {
            StoreController stroreController = new StoreController();
            stroreController.CreateStore();
        }

        [TestMethod]
        public void TestCreateBasket()
        {
            Basket basket = BasketController.CreateBasket("milk bread beans");

            Assert.AreEqual(3, basket.Products.Count);
        }
    }
}

using System;
using System.Collections.Generic;
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
        public void TestInit()
        {
            StoreController stroreController = new StoreController();
            stroreController.CreateStore();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            StoreController stroreController = new StoreController();
            stroreController.ResetStore();
        }

        [TestMethod]
        public void TestCreateBasket()
        {
            Basket basket = BasketController.CreateBasket("milk bread beans");

            Assert.AreEqual(3, basket.Products.Count);
        }

        [TestMethod]
        public void TestNoOfferBasket()
        {
            Basket basket = BasketController.CreateBasket("milk bread beans");
            IList<string> messages = BasketController.GetDiscountApplyedMessages(basket);

            Assert.AreEqual("(No offers available)", messages[0]);
        }

        [TestMethod]
        public void TestApplesOfferBasket()
        {
            Basket basket = BasketController.CreateBasket("apples");
            IList<string> messages = BasketController.GetDiscountApplyedMessages(basket);

            Assert.AreEqual("Apples 10% off: -10p", messages[0]);
        }

        [TestMethod]
        public void TestApplesBasketCost()
        {
            Basket basket = BasketController.CreateBasket("apples apples apples");

            decimal totalCostBasket = BasketController.CalculateBasketCost(basket);
            decimal discountApplyed = BasketController.CalculateDiscountApplyedCost(basket);

            IList<string> messages = BasketController.GetDiscountApplyedMessages(basket);

            Assert.AreEqual(3, totalCostBasket);
            Assert.AreEqual(0.3m, discountApplyed);
            Assert.AreEqual("Apples 10% off: -10p", messages[0]);
            Assert.AreEqual("Apples 10% off: -10p", messages[1]);
            Assert.AreEqual("Apples 10% off: -10p", messages[2]);
        }

        [TestMethod]
        public void TestOneBredOneBeansBasketCost()
        {
            Basket basket = BasketController.CreateBasket("bread beans");

            decimal totalCostBasket = BasketController.CalculateBasketCost(basket);
            decimal discountApplyed = BasketController.CalculateDiscountApplyedCost(basket);

            IList<string> messages = BasketController.GetDiscountApplyedMessages(basket);

            Assert.AreEqual(1.45m, totalCostBasket);
            Assert.AreEqual(0, discountApplyed);
            Assert.AreEqual("(No offers available)", messages[0]);
        }

        [TestMethod]
        public void TestOneBredTwoBeansBasketCost()
        {
            Basket basket = BasketController.CreateBasket("bread beans beans");

            decimal totalCostBasket = BasketController.CalculateBasketCost(basket);
            decimal discountApplyed = BasketController.CalculateDiscountApplyedCost(basket);

            IList<string> messages = BasketController.GetDiscountApplyedMessages(basket);

            Assert.AreEqual(2.10m, totalCostBasket);
            Assert.AreEqual(0.4m, discountApplyed);
            Assert.AreEqual(1, messages.Count);
            Assert.AreEqual("Bread 50% off: -40p", messages[0]);
        }

        [TestMethod]
        public void TestTwoBredTwoBeansBasketCost()
        {
            Basket basket = BasketController.CreateBasket("bread bread beans beans");

            decimal totalCostBasket = BasketController.CalculateBasketCost(basket);
            decimal discountApplyed = BasketController.CalculateDiscountApplyedCost(basket);

            IList<string> messages = BasketController.GetDiscountApplyedMessages(basket);

            Assert.AreEqual(2.90m, totalCostBasket);
            Assert.AreEqual(0.4m, discountApplyed);
            Assert.AreEqual(1, messages.Count);
            Assert.AreEqual("Bread 50% off: -40p", messages[0]);
        }

        [TestMethod]
        public void TestTwoBredThreeBeansBasketCost()
        {
            Basket basket = BasketController.CreateBasket("bread bread beans beans beans");

            decimal totalCostBasket = BasketController.CalculateBasketCost(basket);
            decimal discountApplyed = BasketController.CalculateDiscountApplyedCost(basket);

            IList<string> messages = BasketController.GetDiscountApplyedMessages(basket);

            Assert.AreEqual(3.55m, totalCostBasket);
            Assert.AreEqual(0.4m, discountApplyed);
            Assert.AreEqual(1, messages.Count);
            Assert.AreEqual("Bread 50% off: -40p", messages[0]);
        }

        [TestMethod]
        public void TestTwoBredFourBeansBasketCost()
        {
            Basket basket = BasketController.CreateBasket("bread bread beans beans beans beans");

            decimal totalCostBasket = BasketController.CalculateBasketCost(basket);
            decimal discountApplyed = BasketController.CalculateDiscountApplyedCost(basket);

            IList<string> messages = BasketController.GetDiscountApplyedMessages(basket);

            Assert.AreEqual(4.20m, totalCostBasket);
            Assert.AreEqual(0.8m, discountApplyed);
            Assert.AreEqual(2, messages.Count);
            Assert.AreEqual("Bread 50% off: -40p", messages[0]);
            Assert.AreEqual("Bread 50% off: -40p", messages[1]);
        }
    }
}



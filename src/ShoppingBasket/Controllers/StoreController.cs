using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingBasket.Model;
using ShoppingBasket.Interface;

namespace ShoppingBasket.Controllers
{
    public class StoreController
    {
        public void CreateStore()
        {
            CreateProducts();
            CreateDiscounts();
        }

        public void ResetStore()
        {
            Store.Reset();
        }

        private void CreateProducts()
        {
            Product beans = new Product("Beans", 0.65m, UnitType.can);
            Product bread = new Product("Bread", 0.8m, UnitType.loaf);
            Product milk = new Product("Milk", 1.30m, UnitType.bottle);
            Product apples = new Product("Apples", 1, UnitType.bag);

            Store.AddProduct(beans);
            Store.AddProduct(bread);
            Store.AddProduct(milk);
            Store.AddProduct(apples);
        }

        private void CreateDiscounts()
        {
            Product apples = Store.GetProductByName("apples");
            if (apples != null)
            {
                IDiscount applesDiscount = new PercentageDiscount(apples.Id, 10);
                Store.AddDiscount(applesDiscount);
            }

            Product beans = Store.GetProductByName("beans");
            Product bread = Store.GetProductByName("bread");
            DiscountRule breadDiscountRule = new DiscountRule(beans.Id, 2);

            if(beans != null && bread != null)
            {
                IDiscount breadDiscount = new MixtPercentageDiscount(bread.Id, 50, breadDiscountRule);
                Store.AddDiscount(breadDiscount);
            }          
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using System.Text;
using ShoppingBasket.Model;
using ShoppingBasket.Interface;
using ShoppingBasket.Extension;

namespace ShoppingBasket.Controllers
{
    public static class BasketController
    {
        /// <summary>
        /// The products are passed as a string and are separated by space
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public static Basket CreateBasket(string productsAsText, char splitChar = ' ') 
        {
            Basket basket = new Basket();
            string[] products = productsAsText.Split(splitChar);

            foreach(string productName in products)
            {
                Product product =  Store.GetProductByName(productName);
                if(product != null)
                {
                    basket.AddProduct(product);
                }
                else
                {
                    Console.WriteLine("A product with name {0} was not found in store", productName);
                }
            }

            return basket;
        }

        public static decimal CalculateBasketCost(Basket basket)
        {
            IList<Product> basketProducts = basket.Products;
            decimal total = basketProducts.Sum(p => p.Price);
            return total;
        }

        public static decimal CalculateDiscountApplyedCost(Basket basket)
        {
            IList<Product> basketProducts = basket.Products;
            decimal totalDiscount = 0;

            IList<IDiscount> discountsApplyed = GetDiscountsApplyed(basket);
            
            foreach (IDiscount discount in discountsApplyed)
            {
                Product basketProduct = basketProducts.FirstOrDefault(p => p.Id == discount.ProductId);
                if (basketProduct != null)
                {
                    string message;
                    decimal discountValue = discount.CalculateDiscount(basketProduct, out message);
                    totalDiscount += discountValue;                       
                }
            }            

            return totalDiscount;
        }

        public static IList<string> GetDiscountApplyedMessages(Basket basket)
        {
            IList<string> messages = new List<string>();

            IList<Product> basketProducts = basket.Products;
            decimal subTotal = basketProducts.Sum(p => p.Price);
            Dictionary<string, int> mappProductsWithConditionalDiscount = new Dictionary<string, int>();
            
            IList<IDiscount> discountsApplyed = GetDiscountsApplyed(basket);

            if(discountsApplyed.Count == 0)
            {
                messages.Add("(No offers available)");
            }
            else
            {
                foreach(IDiscount discount in discountsApplyed)
                {                    
                    Product basketProduct = basketProducts.FirstOrDefault(p => p.Id == discount.ProductId);
                    if(basketProduct != null)
                    {
                        string message;
                        decimal discountValue = discount.CalculateDiscount(basketProduct, out message);
                        messages.Add(message);                        
                    }                    
                }
            }

            return messages;
        }

        public static IList<IDiscount> GetDiscountsApplyed(Basket basket)
        {
            IList<IDiscount> discountsApplyed = new List<IDiscount>();
            IList<Product> basketProducts = basket.Products;
            Dictionary<string, int> mappProductsWithConditionalDiscount = new Dictionary<string, int>();

            foreach (Product product in basketProducts)
            {
                if (product == null)
                    continue;

                IDiscount discount = GetDiscountOfProduct(product);

                int productOccurence = 1;
                if (mappProductsWithConditionalDiscount.ContainsKey(product.Id))
                {
                    productOccurence = mappProductsWithConditionalDiscount[product.Id] + 1;
                }

                bool canApplyDiscount = CanApplyDiscount(basket, product, discount, productOccurence);
                if (canApplyDiscount)
                {
                    if (mappProductsWithConditionalDiscount.ContainsKey(product.Id))
                    {
                        mappProductsWithConditionalDiscount[product.Id] = mappProductsWithConditionalDiscount[product.Id]++;
                    }
                    else
                    {
                        mappProductsWithConditionalDiscount[product.Id] = 1;
                    }
                    discountsApplyed.Add(discount);
                }
            }

            return discountsApplyed;
        }

        public static IDiscount GetDiscountOfProduct(Product product)
        {
            IDiscount discount = Store.GetDiscountByProductId(product.Id);
            return discount;
        }

        public static bool CanApplyDiscount(Basket basket, Product product, IDiscount discount, int productOccurence)
        {
            if (basket == null || product == null || discount == null)
                return false;

            if (!discount.IsActive)
                return false;

            IConditionalDiscount conditionalDiscount = discount as IConditionalDiscount;
            if (conditionalDiscount != null)
            {
                int count = basket.CountSameProductsInBasket(conditionalDiscount.Rule.ProductId);
                return productOccurence <= (int)(count / conditionalDiscount.Rule.Occurence);
            }
            return true;
        }
    }
}

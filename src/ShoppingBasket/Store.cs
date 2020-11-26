using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ShoppingBasket.Interface;
using ShoppingBasket.Model;

namespace ShoppingBasket
{
    public class Store
    {
        private static Store _instance;
        private static IList<Product> _products = new List<Product>();
        private static IList<IDiscount> _discounts = new List<IDiscount>();
        private Store()
        {
        }

        public static Store Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Store();
                }

                return _instance;
            }
        }

        public static void Reset()
        {
            _products = new List<Product>();
        }

        public static IList<Product> AllProducts()
        {
            // do not expose the list with products            
            return new List<Product>(_products);           
        }


        public static void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new Exception("Null product can't be added");
            }
            if (_products.Any(p => p.Name == product.Name))
            {
                throw new Exception(String.Format("A product with name {0} already exist", product.Name));
            }

            _products.Add(product);
        }

        public static Product GetProductByName(string name)
        {
            string lowerCaseName = string.IsNullOrWhiteSpace(name) ? "" : name.ToLower();
            Product product = _products.FirstOrDefault(p => p.Name.ToLower() == lowerCaseName);

            return product;
        }

        public static Product GetProductById(string id)
        {
            string lowerCaseId = string.IsNullOrWhiteSpace(id) ? "" : id.ToLower();
            Product product = _products.FirstOrDefault(p => p.Id.ToLower() == lowerCaseId);

            return product;
        }

        public static bool RemoveProductById(string id)
        {
            Product product = GetProductById(id);
            if (product != null)
            {
                _products.Remove(product);
                return true;
            }

            return false;
        }

        public static void AddDiscount(IDiscount discount)
        {
            if (discount == null)
            {
                throw new Exception("Null discount can't be added");
            }
            if (_discounts.Any(d => d.ProductId == discount.ProductId))
            {
                throw new Exception("A discount for this product already exist " + discount.ProductId);
            }

            _discounts.Add(discount);
        }

        public static IDiscount GetDiscountByProductId(string productId)
        {
            string lowerCaseProductId = string.IsNullOrWhiteSpace(productId) ? "" : productId.ToLower();
            IDiscount discount = _discounts.FirstOrDefault(p => p.ProductId.ToLower() == lowerCaseProductId);

            return discount;
        }


        public static bool RemoveDiscountByProductId(string productId)
        {
            IDiscount discount = GetDiscountByProductId(productId);
            if (discount != null)
            {
                _discounts.Remove(discount);
                return true;
            }

            return false;
        }
    }
}

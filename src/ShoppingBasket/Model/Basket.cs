using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ShoppingBasket.Model
{
    public class Basket
    {
        private IList<Product> _products = new List<Product>();

        public IList<Product> Products
        {
            // do not expose the basket list
            get
            {
                return new List<Product>(_products);
            }
        }
        public void AddProduct(Product product)
        {
            if(product != null)
            {
                _products.Add(product);
            }            
        }

        public int CountSameProductsInBasket(string productId)
        {            
            int count = _products.Where(p => p.Id == productId).Count();

            return count;
        }

        public bool RemoveProductByName(string name)
        {
            string lowerCaseName = string.IsNullOrWhiteSpace(name) ? "" : name.ToLower().Trim();
            Product product = _products.FirstOrDefault(p => p.Name.ToLower() == lowerCaseName);
            if (product != null)
            {
                _products.Remove(product);
                return true;
            }

            return false;
        }
    }
}

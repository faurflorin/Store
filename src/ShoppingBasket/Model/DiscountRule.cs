using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket.Model
{
    public class DiscountRule
    {
        private string _productId;
        private int _occurence;
        public DiscountRule(string productId, int occurence)
        {
            ProductId = productId;
            Occurence = occurence;
        }

        public string ProductId
        {
            get
            {
                return _productId;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Product id can't be empty or null");
                }
                _productId = value;
            }
        }
        public int Occurence {
            get
            {
                return _occurence;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Occurence should be a positive value");
                }
                _occurence = value;
            }
        }
    }
}


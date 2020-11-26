using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingBasket.Enums;
using ShoppingBasket.Interface;
using ShoppingBasket.Extension;

namespace ShoppingBasket.Model
{
    public abstract class Discount : IDiscount
    {
        private string _productId;
        protected decimal _value;
        public Discount(string productId, decimal value)
        {
            ProductId = productId;
            Value = value;
        }

        public abstract decimal CalculateDiscount(Product product, out string message);
        public abstract decimal Value { get; set; }

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
        
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public bool IsActive
        {
            get
            {
                // promotion not start yet
                if (ValidFrom.HasValue && ValidFrom > DateTime.Now)
                    return false;

                // promotion end
                if (ValidTo.HasValue && ValidFrom < DateTime.Now)
                    return false;

                return true;
            }
        }
    }

    public class ValueDiscount : Discount
    {
        public ValueDiscount(string productId, decimal value) : base(productId, value)
        {
        }

        public override decimal Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Discount should has a positive value");
                }
                _value = value;
            }
        }

        public override decimal CalculateDiscount(Product product, out string message)
        {
            decimal discountValue = 0;
            if (IsActive)
            {
                discountValue = product.Price < Value ? product.Price : Value; // discount value applyed can not be greated the product price                
            }

            message = String.Format("{0} {1} off: -{2}", product.Name, Value.ToUkFormat(), discountValue.ToUkFormat());
            return discountValue;
        }
    }

    public class PercentageDiscount : Discount
    {
        public PercentageDiscount(string productId, decimal value) : base(productId, value)
        {
        }

        public override decimal Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value <= 0 || value >= 100)
                {
                    throw new Exception("Discount should has a positive value and be between 0 and 100");
                }
                _value = value;
            }
        }

        public override decimal CalculateDiscount(Product product, out string message)
        {
            decimal discountValue = 0;
            if (IsActive)
            {
                discountValue = product.Price * (decimal)(Value / 100);
            }
            message = String.Format("{0} {1}% off: -{2}", product.Name, Value, discountValue.ToUkFormat());
            return discountValue;
        }
    }

    public class MixtValueDiscount : ValueDiscount, IConditionalDiscount
    {
        public MixtValueDiscount(string productId, decimal value, DiscountRule rule) : base(productId, value)
        {
            Rule = rule;
        }

        public DiscountRule Rule { get; set; }
    }

    public class MixtPercentageDiscount : PercentageDiscount, IConditionalDiscount
    {
        public MixtPercentageDiscount(string productId, decimal value, DiscountRule rule) : base(productId, value)
        {
            Rule = rule;
        }

        public DiscountRule Rule { get; set; }
    }
}

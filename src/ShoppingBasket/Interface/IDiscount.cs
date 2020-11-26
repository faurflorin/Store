using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingBasket.Model;

namespace ShoppingBasket.Interface
{
    public interface IDiscount
    {
        string ProductId { get; }
        decimal Value { get; set; }
        DateTime? ValidFrom { get; set; }
        DateTime? ValidTo { get; set; }

        bool IsActive { get; }

        decimal CalculateDiscount(Product product, out string message);
    }

    public interface IConditionalDiscount
    {
       DiscountRule Rule { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket.Extension
{
    public static partial class Extensions
    {
        public static string ToUkFormat(this Decimal value)
        {
            if (value < 1)
            {
                return String.Format("{0:0}p", value * 100);
            }
            return String.Format("\u00A3{0:0.00}", value).Replace(",", "."); //\u00A3 pound symbol
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket.Model
{
    public class Product
    {
        private string _name;
        private decimal _price;
        public Product(string name, decimal price, UnitType unitType)
        {
            Id = Guid.NewGuid().ToString("N");
            Name = name.Trim();
            Price = price;
            Unit = unitType;
        }

        public string Id { get; private set; }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Product name can't be empty or null");
                }
                _name = value.Trim();
            }
        }
        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Price should have a positive value");
                }
                _price = value;
            }
        }
        public UnitType Unit { get; set; }
    }
}

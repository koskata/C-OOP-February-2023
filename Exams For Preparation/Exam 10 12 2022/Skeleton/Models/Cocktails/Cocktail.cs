using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;

namespace ChristmasPastryShop.Models.Cocktails
{
    public abstract class Cocktail : ICocktail
    {
        private string name;
        private string size;
        private double price;
        private string[] sizes = { "Small", "Middle", "Large" };

        public Cocktail(string cocktailName, string size, double price)
        {
            Name = cocktailName;
            Size = size;
            Price = price;
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                name = value;
            }
        }

        public string Size
        {
            get { return size; }
            private set
            {
                //TODO in Controller class - checker if is correct size

                size = value;
            }
        }

        public double Price
        {
            get { return price; }
            private set
            {
                if (Size == "Large")
                {
                    price = value;
                }
                if (Size == "Middle")
                {
                    price = value / 1.5;
                }
                if (Size == "Small")
                {
                    price = value / 3;
                }

            }
        }

        public override string ToString()
        {
            return $"{Name} ({Size}) - {Price:f2} lv";
        }
    }
}

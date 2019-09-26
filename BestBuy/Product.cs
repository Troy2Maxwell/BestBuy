using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuy
{
    class Product : IProduct
    {
        public double Price { get; set; }
        public string Name { get; set; }        
        public Product(string name, double price)
        {
            //Constructor get's and set's Product Name and Product Price
            Name = name;
            Price = price;
        }
        public double PriceWithTax()
        {
            Price *= 1.1;
            Price = Math.Round(Price, 2);
            return Price;
        }
}
}

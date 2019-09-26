using System;

namespace BestBuy
{
    class Program
    {

        static void Main(string[] args)
        {
            Product newProduct = new Product("IPhone", 349.99);
            /*newProduct.Name = "Iphone";
            newProduct.Price = 349;
            newProduct.PriceWithTax();*/

            Console.WriteLine($"{newProduct.Name}, Price: ${newProduct.Price}, Price with tax:{newProduct.PriceWithTax()}");
        }
    }
}

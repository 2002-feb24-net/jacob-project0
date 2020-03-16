using System.Collections.Generic;
using System;
namespace Project0
{
    static class Store
    {
        static Io _io = new Io();
        static List<Product> productList = new List<Product>()
        {
            new Product ("product1","date",15,10),
            new Product ("product2","date",20,10),
            new Product ("product3","date",20,10),
            new Product ("product4","date",20,10),
        };
        public static string orderSelection()
        {
            while (true)
            {
                foreach (Product item in productList)
                {
                    _io.Output(item._name + "  $" + item._price + "\n");
                }
                _io.Output("Enter your choice: ");
                string choice = _io.Input();
                if(Product.productNames.Contains(choice))
                {
                    DateTime timeStamp = DateTime.Now;
                    return  timeStamp.ToString("F") + "|" + choice;
                }
                else
                {
                    _io.Output("Invalid input. \n");
                }
            }
        }
        public static double GetPrice(string input)
        {
            foreach (var item in productList)
            {
                if(item._name == input)
                {
                    return item._price;
                }
            }
            return 0;
        }
    }
}
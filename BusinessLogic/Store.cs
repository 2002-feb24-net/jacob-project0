using System.Collections.Generic;
using System;
namespace BusinessLogic
{
    static class Store
    {
        static List<Products> productList = new List<Products>()
        {
            new Products ("product1",15,10),
            new Products ("product2",20,10),
            new Products ("product3",20,10),
            new Products ("product4",20,10),
        };
        public static string orderSelection()
        {
            while (true)
            {
                foreach (Products item in productList)
                {
                    _io.Output(item.GetName() + "  $" + item.GetPrice() + "\n");
                }
                _io.Output("Enter your choice: ");
                string choice = _io.Input();
                if(Products.CheckNames(choice))
                {
                    DateTime timeStamp = DateTime.Now;
                    return  timeStamp.ToString("F") + "|" + choice;
                }
                else
                {
                    Console.WriteLine("Invalid input. \n");
                }
            }
        }
        public static double GetPrice(string input)
        {
            foreach (var item in productList)
            {
                if(item.GetName() == input)
                {
                    return item.GetPrice();
                }
            }
            return 0;
        }
    }
}
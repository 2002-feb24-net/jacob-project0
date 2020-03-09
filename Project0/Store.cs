using System.Collections.Generic;
using System;
namespace Project0
{
    class Store
    {
        static Dictionary<string,double> productList = new Dictionary<string, double>()
        {
            { "product" , 1000.00 },
            { "product" , 1000.00 },
            { "product" , 1000.00 },
            { "product" , 1000.00 }
        };
        public string orderSelection()
        {
            while (true)
            {
                Console.WriteLine("What product would you like to buy today?");
                foreach (var item in productList)
                {
                    Console.WriteLine(item.Key + "  $" + item.Value);
                }
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine().ToLower();
                if(productList.ContainsKey(choice))
                {
                    DateTime timeStamp = DateTime.Today;
                    return  timeStamp.ToString("d") +"|"+ timeStamp.ToString("g").Substring(9).Trim(' ') + " " + choice;
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }
        public double getPrice(string input)
        {
            double price;
            if(productList.TryGetValue(input, out price))
            {
                return price;
            }
            return 0;
        }
    }
}
using System;
using System.Collections.Generic;
//Project 0 By Jacob Koch
namespace Project0
{
    class Program
    {
        List<Customer> customerList = new List<Customer>()
        {
            new Customer("username","password"),
            new Customer("bob","cat"),
            new Customer("sarah","birthday"),
            new Customer("destiny","child")
        };
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the store store: ");
        }
    }
}

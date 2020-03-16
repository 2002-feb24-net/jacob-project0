using System;
using System.Collections.Generic;
using System.Text;

namespace Project0
{
    class Product
    {
        public string _name { get; set; }
        public string _timeStamp { get; set; }
        public int _stock { get; set; }
        public double _price { get; set; }
        public static List<string> productNames = new List<string>();
        public Product()
        {

        }
        public Product(string name, string time, int stock, double price)
        {
            this._name = name.ToLower();
            this._timeStamp = time;
            this._stock = stock;
            this._price = price;
            productNames.Add(name.ToLower());
        }
    }
}

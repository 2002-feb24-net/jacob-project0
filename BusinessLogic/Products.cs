using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    class Products
    {
        protected string _name { get; set; }
        protected int _stock { get; set; }
        protected double _price { get; set; }
        static List<string> productNames = new List<string>();
        public Products()
        {

        }
        public Products(string name, int stock, double price)
        {
            this._name = name.ToLower();
            this._stock = stock;
            this._price = price;
            productNames.Add(name.ToLower());
        }
        public string GetName()
        {
            return _name;
        }

        public double GetPrice()
        {
            return _price;
        }
        public static bool CheckNames(string str)
        {
            if(productNames.Contains(str))
            {
                return true;
            }
            return false;
        }
    }
}

using System.Collections.Generic;
using System;
namespace Project0
{
    class Customer
    {
        //orderHistory is private because it is unique for each customer
        Dictionary<string,string> orderHistory = new Dictionary<string,string>();
        public string _name { get; set; }
        public string _password { get; set; }
        public Customer()
        {

        }
        public Customer(string name, string password)
        {
            this._name = name;
            this._password = password;
        }
        public void Order()
        {
            string[] orderTemp = (Store.orderSelection()).Split("|");
            orderHistory.Add(orderTemp[0],orderTemp[1]);
            //string 1 is date
            //string 2 is product
        }
        public string printOrderHistory()
        {
            string returnString = "";
            foreach (var item in orderHistory)
            {
                returnString += item.Key + " " + item.Value+" $"+Store.GetPrice(item.Value) + "\n";
            }
            return returnString;
        }
        public override string ToString()
        {
            return _name + " " + _password;
        }
    }
}
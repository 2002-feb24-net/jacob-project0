using System.Collections.Generic;
using System;
namespace BusinessLogic
{
    public class Customer
    {
        string _name { get; }
        string _lastName { get; }
        public Customer()
        {

        }
        public Customer(string name, string lastName)
        {
            this._name = name;
            this._lastName = lastName;
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
            return _name + " " + _lastName;
        }
    }
}
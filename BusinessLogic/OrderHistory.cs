using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    class OrderHistory : Products
    {
        string _timeStamp { get; }
        public OrderHistory(string name, string timeStamp, double price)
        {
            this._name = name.ToLower();
            this._price = price;
            this._timeStamp = timeStamp;
        }
    }
}

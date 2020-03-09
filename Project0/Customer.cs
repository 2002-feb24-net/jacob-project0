using System.Collections.Generic;
namespace Project0
{
    class Customer
    {
        //orderHistory is private because it is unique for each customer
        Dictionary<string,string> orderHistory = new Dictionary<string,string>();
        //loginDirectory is static because it is the same for all instances
        static Dictionary<string,string> loginDirectory = new Dictionary<string,string>();
        static Store supremeStore = new Store();
        public Customer(string name, string password)
        {
            loginDirectory.Add(name,password);
        }
        public void Order()
        {
            string[] orderTemp = (supremeStore.orderSelection()).Split(" ");
            orderHistory.Add(orderTemp[0],orderTemp[1]);
            //string 1 is date
            //string 2 is product
        }
        static string CustomerLogin()
        {
            return "";
        }
    }
}
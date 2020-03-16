using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//Project 0 By Jacob Koch
namespace Project0
{
    class Program
    {
        static int currentCustomer = -1;

        static List<Customer> customerList = null;
        static Io _io = new Io();
        static async Task Main(string[] args)
        {
            //put this into a method
            string jsonData = await File.ReadAllTextAsync("../../../customer.json");
            customerList = JsonSerializer.Deserialize<List<Customer>>(jsonData);
            //
            _io.Output("Welcome to the store store! \n");
            _io.Output("Are you an existing member? (Y/N)");
            string input = _io.Input();
            if(input=="y")
            {
                LogIn();
            }
            else
            {
                SignUp();
            }
            //access store for specific customer
            _io.Output("What product would you like to buy today? \n");
            customerList[currentCustomer].Order();
            _io.Output("Thank you for your order! \n");
            _io.Output("Order History: \n");
            _io.Output(customerList[currentCustomer].printOrderHistory() + "\n");
            //put this into a method
            string text = JsonSerializer.Serialize(customerList);
            await WriteToFileAsync(text , "../../../customer.json");
            //
        }
        static void SignUp()
        {
            _io.Output("Sign Up: \n");
            _io.Output("Enter a new username: ");
            string userName = _io.Input();
            //add check if username already exists
            _io.Output("Enter a new password: ");
            string password = _io.Input();
            currentCustomer = customerList.Count;
            customerList.Add(new Customer(userName,password));
        }
        static void LogIn()
        {
            _io.Output("Enter your username: ");
            string userName = _io.Input();
            _io.Output("Enter your password: ");
            string password = _io.Input();
            string comparisonString = userName + " " + password;
            //maybe try if(customerList.Contains(new Customer(userName,password)));
            for(int i = 0; i < customerList.Count; i++)
            {
                if(customerList[i].ToString() == comparisonString)
                {
                    currentCustomer = i;
                }
            }
            if(currentCustomer == -1)
            {
                _io.Output("Error: Username or Password do not match. \n");
            }
        }
        async static Task WriteToJsonAsync(List<Customer> data, string filePath)
        {
            string text = JsonSerializer.Serialize(data);
            await File.WriteAllTextAsync(filePath, text);
        }
        async static Task<string> ReadFromFileAsync(string filePath)
        {
            var sr = new StreamReader(filePath);
            Task<string> textTask = sr.ReadToEndAsync();
            string text = await textTask;
            sr.Close();
            return text;
        }
        static async Task WriteToFileAsync(string text, string filePath)
        {
            FileStream file = null;
            try
            {
                file = new FileStream(filePath, FileMode.Create);
                //File.WriteAllText("../../../data.json",text);
                byte[] data = Encoding.UTF8.GetBytes(text);
                await file.WriteAsync(data);
                file.Close();
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access to file {filePath} is not allowed by the OS: ");
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                if (file != null)
                {
                    file.Dispose();
                }
            }
        }
    }
}

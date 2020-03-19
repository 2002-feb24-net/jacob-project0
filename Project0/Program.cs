using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLogic;
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
            _io.Output("Welcome to the store store! \n");
            _io.Output("Are you an existing member? (Y/N)");
            string input = _io.Input();
            if(input=="y")
            {
                LogIn();
            }
            else
            {
                NewUser();
            }
            //access store for specific customer
            _io.Output("What product would you like to buy today? \n");
            customerList[currentCustomer].Order();
            _io.Output("Thank you for your order! \n");
            _io.Output("Order History: \n");
            _io.Output(customerList[currentCustomer].printOrderHistory() + "\n");
        }
        async void JsonLoader()
        {
            //change filepath to parameter
            string jsonData = await File.ReadAllTextAsync("../../../customer.json");
            customerList = JsonSerializer.Deserialize<List<Customer>>(jsonData);
        }
        async void JsonUploader()
        {
            //change filepath to parameter
            string text = JsonSerializer.Serialize(customerList);
            await WriteToFileAsync(text, "../../../customer.json");
        }
        static void NewUser()
        {
            _io.Output("Adding New User: \n");
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
            _io.Output("Enter your firstName: ");
            string firstName = _io.Input();
            _io.Output("Enter your lastName: ");
            string lastName = _io.Input();
            string comparisonString = firstName + " " + lastName;
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
                _io.Output("Error: Firstname or Lastname do not match. \n");
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

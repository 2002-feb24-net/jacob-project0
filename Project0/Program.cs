using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using Project0.Library.Model;
using Project0.Library.Interfaces;
using Project0.ConsoleUI.ConsoleIO;
using NLog;

//Project 0 By Jacob Koch
namespace Project0.ConsoleUI
{
    class Program
    {
        private static readonly NLog.ILogger s_logger = LogManager.GetCurrentClassLogger();
        static Io _io = new Io();
        //static async Task Main(string[] args)
        static void Main(string[] args)
        {
            var dependencies = new Dependencies();
            using IProject0Repository projectRepository = dependencies.
                CreateProject0Repository();
            _io.Output("Welcome to the latest in Food Delivery! \n");
            bool loopBool = true;
            while(loopBool)
            {
                _io.Output("Would you like to place an order? (order)\n");
                _io.Output("Add a new customer? (new)\n");
                _io.Output("Display a specific order? (specific)\n");
                _io.Output("Display all order history of a store location? (location)\n");
                _io.Output("Display all order history of a customer? (customer)\n");
                _io.Output("Quit? (quit)\n");
                string input = _io.Input().ToLower();
                if (input == "order")
                {
                    var customer = LogIn(projectRepository);
                    MakeOrder(customer, projectRepository);
                }
                else if (input == "new")
                {
                    NewUser(projectRepository);
                }
                else if (input == "specific")
                {
                    DisplaySpecificOrder(projectRepository);
                }
                else if (input == "location")
                {
                    DisplayLocationHistory(projectRepository);
                }
                else if (input == "customer")
                {
                    DisplayCustomerHistory(projectRepository);
                }
                else if (input == "quit")
                {
                    loopBool = false;
                }
                else
                {
                    _io.Output("Invalid Input. \n");
                    //invalid input
                    //repeat
                }
            }
        }
        static Customer LogIn(IProject0Repository projectRepository)
        {
            var customer = new Customer();
            var customerList = projectRepository.GetCustomers(); 
            while (customer.FirstName == null)
            {
                _io.Output("Enter your firstName: ");
                string firstName = _io.Input();
                _io.Output("Enter your lastName: ");
                string lastName = _io.Input();
                try
                {
                    var test = customerList.First(c => c.FirstName.ToLower() == firstName && c.LastName.ToLower() == lastName);
                    customer = test;
                }
                catch (ArgumentException ex)
                {
                    _io.Output("Your name does not match our database.");
                    s_logger.Info(ex);
                    Console.WriteLine(ex.Message);
                }
            }
            return customer;
        }

        static void NewUser(IProject0Repository projectRepository)
        {
            var customer = new Customer();
            while (customer.FirstName == null || customer.LastName == null)
            {
                _io.Output("Adding New User: \n");
                _io.Output("Enter a new firstName: ");
                string firstName = _io.Input();
                _io.Output("Enter a new lastName: ");
                string lastName = _io.Input();
                try
                {
                    customer.FirstName = firstName;
                    customer.LastName = lastName;
                }
                catch (ArgumentException ex)
                {
                    s_logger.Info(ex);
                    Console.WriteLine(ex.Message);
                }
            }
            _io.Output("User Added. \n \n");
            projectRepository.AddCustomer(customer);
            projectRepository.Save();
        }
        static void MakeOrder(Customer customer, IProject0Repository projectRepository)
        {
            _io.Output("Choose a location: \n");
            //display all locations
            var locations = projectRepository.GetLocations();
            var locationList = locations.ToList<StoreLocation>();
            int locationId = 0;
            while(locationId == 0)
            {
                foreach (var item in locationList)
                {
                    _io.Output(item.LocationName + "\n");
                }
                string locationInput = _io.Input();
                try
                {
                    //get store id
                    locationId = locations.First(l => l.LocationName.ToLower() == locationInput).Id;
                }
                catch (ArgumentException ex)
                {
                    _io.Output("Store name input invalid.");
                    s_logger.Info(ex);
                    Console.WriteLine(ex.Message);
                }
            }
            //display available products
            _io.Output("Choose a product: \n");
            var products = projectRepository.GetProducts();
            var productList = products.Where(p => p.StoreLocationId == locationId && p.Stock > 0).ToList<Product>();
            var productObject = new Product();
            while(productObject.Name == null)
            {
                foreach (var item in productList)
                {
                    _io.Output($"Name: {item.Name} Stock: {item.Stock} Price: {item.Price} \n");
                }
                var productInput = _io.Input();
                try
                {
                    productObject = productList.First(p => p.Name.ToLower() == productInput);
                }
                catch (Exception ex)
                {
                    _io.Output("Product name or Quantity input invalid. ");
                    s_logger.Info(ex);
                    Console.WriteLine(ex.Message);
                }
            }

            var quantityInput = 0;
            var quantityBool = true;
            while (quantityBool)
            {
                _io.Output("Choose a quantity: \n");
                quantityInput = Int32.Parse(_io.Input());
                if (quantityInput > productObject.Stock)
                {
                    _io.Output("Quantity too large. \n");
                }
                else
                {
                    quantityBool = false;
                }
            }
            productObject.Stock = productObject.Stock - quantityInput;
            projectRepository.UpdateProduct(productObject);
            DateTime timeStamp = DateTime.Now;
            var newOrder = new Orders { StoreLocationId = locationId, CustomerId = customer.Id, ProductId = productObject.Id, Quantity = quantityInput };
            projectRepository.AddOrder(newOrder);
            projectRepository.Save();
            _io.Output("Order Complete.");

        }
        static void DisplayLocationHistory(IProject0Repository projectRepository)
        {
            _io.Output("Select an Location: \n");
            var locations = projectRepository.GetLocations();
            var locationList = locations.ToList<StoreLocation>();
            int locationId = 0;
            while(locationId == 0)
            {
                foreach (var item in locationList)
                {
                    _io.Output(item.LocationName + "\n");
                }
                string locationInput = _io.Input();
                try
                {
                    //get store id
                    locationId = locations.First(l => l.LocationName.ToLower() == locationInput).Id;
                }
                catch (ArgumentException ex)
                {
                    _io.Output("Location Id Invalid.");
                    s_logger.Info(ex);
                    Console.WriteLine(ex.Message);
                }
            }
            var orderHistory = projectRepository.GetOrders().Where(o => o.StoreLocationId == locationId).ToList();
            foreach (var item in orderHistory)
            {
                var productName = projectRepository.GetProductById(item.ProductId).Name;
                var customerName = projectRepository.GetCustomerById(item.CustomerId).FirstName + " " + projectRepository.GetCustomerById(item.CustomerId).LastName;
                var locationName = projectRepository.GetLocationsById(item.CustomerId).LocationName;
                _io.Output($"ID: {item.Id} Product Name: {productName} Quantity: {item.Quantity} " +
                            $" Customer Name: {customerName} \n");
            }
        }
        static void DisplayCustomerHistory(IProject0Repository projectRepository)
        {
            _io.Output("Select an Customer: \n");
            var customers = projectRepository.GetCustomers();
            var customerList = customers.ToList<Customer>();
            int customerId = 0;
            while(customerId == 0)
            {
                foreach (var item in customerList)
                {
                    _io.Output($"{item.FirstName} {item.LastName} \n");
                }
                string[] customerInput = _io.Input().Split(' ');
                try
                {
                    customerId = customers.First(c => c.FirstName.ToLower() == customerInput[0] && c.LastName.ToLower() == customerInput[1]).Id;
                }
                catch (ArgumentException ex)
                {
                    _io.Output("Name Invalid. It does not match any in our database.");
                    s_logger.Info(ex);
                    Console.WriteLine(ex.Message);
                }
            }
            var orderHistory = projectRepository.GetOrders().Where(o => o.CustomerId == customerId).ToList();
            foreach (var item in orderHistory)
            {
                var productName = projectRepository.GetProductById(item.ProductId).Name;
                var customerName = projectRepository.GetCustomerById(item.CustomerId).FirstName + " " + projectRepository.GetCustomerById(item.CustomerId).LastName;
                //var locationName = projectRepository.GetLocationsById(item.CustomerId).LocationName;
                //_io.Output($"ID: {item.Id} Product Name: {productName} Quantity: {item.Quantity} " +
                //            $"Location: {locationName} Customer Name: {customerName} \n");
                _io.Output($"ID: {item.Id} Product Name: {productName} Quantity: {item.Quantity} " +
                            $" Customer Name: {customerName} \n");
            }
        }
        static void DisplaySpecificOrder(IProject0Repository projectRepository)
        {
            int orderId = 0;
            var specificOrder = new Orders();
            while (orderId == 0 && specificOrder.Id == 0)
            {
                _io.Output("Enter Order Id: \n");
                try
                {
                    orderId = Int32.Parse(_io.Input());
                    specificOrder = projectRepository.GetOrders().First(o => o.Id == orderId);
                }
                catch (ArgumentException ex)
                {
                    _io.Output("Order Id Invalid.");
                    s_logger.Info(ex);
                    Console.WriteLine(ex.Message);
                }
            }
            var productName = projectRepository.GetProductById(specificOrder.ProductId).Name;
            var customerName = projectRepository.GetCustomerById(specificOrder.CustomerId).FirstName +" "+ projectRepository.GetCustomerById(specificOrder.CustomerId).LastName;
            // locationName = projectRepository.GetLocationsById(specificOrder.CustomerId).LocationName;
            //_io.Output($"ID: {specificOrder.Id} Product Name: {productName} Quantity: {specificOrder.Quantity} " +
            // $"Location: {locationName} Customer Name: {customerName} \n");
            _io.Output($"ID: {specificOrder.Id} Product Name: {productName} Quantity: {specificOrder.Quantity} " +
             $" Customer Name: {customerName} \n");
        }


        // Serializer Code Below vvv
        /*
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
        */
    }
}

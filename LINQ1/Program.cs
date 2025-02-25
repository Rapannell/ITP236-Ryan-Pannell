using LINQ1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the list of customers
            var customers = CustomerData.Customers;

            // Display details for each customer
            foreach (var customer in customers)
            {
                DisplayCustomerDetails(customer);
            }

            // Display summary for all customers
            DisplaySummary(customers);
        }

        static void DisplayCustomerDetails(Customer customer)
        {
            Console.WriteLine("Customer Name: " + customer.Name);
            Console.WriteLine("Total Order Value: " + customer.OrderTotal.ToString("C"));
            Console.WriteLine("Backordered Quantity: " + customer.BackOrdered);
            Console.WriteLine("Average Order Size: " + customer.SalesOrders.Average(order => order.OrderTotal).ToString("C"));
            Console.WriteLine(new string('-', 40));
        }

        static void DisplaySummary(List<Customer> customers)
        {
            // Calculate average order size for all customers
            double averageOrderSize = customers.SelectMany(c => c.SalesOrders).Average(order => order.OrderTotal);

            // Find the customer with the highest OrderTotal
            var customerWithHighestTotal = customers.OrderByDescending(c => c.OrderTotal).First();

            Console.WriteLine("\nSummary for All Customers:");
            Console.WriteLine("Average Order Size: " + averageOrderSize.ToString("C"));
            Console.WriteLine("Customer with Highest Order Total: " + customerWithHighestTotal.Name + " (" + customerWithHighestTotal.OrderTotal.ToString("C") + ")");
        }
    }
}
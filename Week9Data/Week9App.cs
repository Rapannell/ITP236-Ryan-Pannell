using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Web;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
//Ryan Pannell

namespace Week9Data
{
    internal class Program
    {
        static List<Customer> customers;
        static List<Part> parts;
        static List<SalesOrder> salesOrders;
        static List<SalesOrderPart> salesOrderParts;
        static string connectionString = "Data Source=164.106.46.15;Initial Catalog=ITP236_08;User ID=ITP236_08;Password=!Jsrrap23641#6376481;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;";
        static void Main(string[] args)
        {
            GetData();
            //CreateXml();
            //AdoNet();
            LoadCustomers(); //--< Populate our Customer table <<<
            CreateParts();
            CreatePartsXml();
        }

        static void LoadCustomers()
        {
            string clearCustomers = "DELETE FROM Sales.Customer";
            string insertCustomer = "INSERT INTO Sales.Customer (FirstName, LastName, City, State) VALUES (@FirstName, @LastName, @City, @State)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    int rowsAffected = 0;
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(clearCustomers, connection))
                    {
                        rowsAffected = command.ExecuteNonQuery();
                        command.CommandText = insertCustomer;
                        foreach(var customer in customers)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                            command.Parameters.AddWithValue("@LastName", customer.LastName);
                            command.Parameters.AddWithValue("@City", customer.City);
                            command.Parameters.AddWithValue("@State", customer.State);
                            rowsAffected = command.ExecuteNonQuery();
                        }
                        
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    Console.WriteLine($"Customer inserts failed {ex.Message}");
                }
            }
        }
        static void AdoNet()
        {
            string iAm = "INSERT INTO Sales.Student(Name) Values " + "(@Name)";
            string deleteMe = "DELETE FROM Sales.Student";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int rowsAffected = 0;
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(iAm, connection))
                    {
                        command.CommandText = deleteMe;
                        command.Parameters.Clear();
                        rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Deleted {rowsAffected} rows.");

                        // Add Parameter
                        command.CommandText = iAm;
                        command.Parameters.AddWithValue("@Name", "Ryan Pannell");
                        rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Inserted {rowsAffected} rows");
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
        static void CreateXml()
        {
            string filePath = "../../SalesData.xml";
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                NewLineOnAttributes = false
            };
            using (XmlWriter writer = XmlWriter.Create(filePath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Customers");
                foreach (var customer in customers)
                {
                    writer.WriteStartElement("Customer");
                    writer.WriteElementString("CustomerId", customer.CustomerId.ToString());
                    writer.WriteElementString("Name", $"{customer.FirstName} {customer.LastName}");
                    writer.WriteElementString("TotalSales", customer.TotalSales.ToString());
                    writer.WriteElementString("ItemsSold", customer.ItemsSold.ToString());
                    writer.WriteElementString("Orders", customer.SalesOrders.Count().ToString());
                    writer.WriteEndElement();

                }
                writer.WriteEndDocument();
            }
        }

        static void CreateParts()
        {
            string insertPart = @"
                INSERT INTO Sales.Part 
                (Name, Price, ReceivedUnits, ShippedUnits, SpoiledUnits, 
                 ReceivedValue, ShippedValue, SpoiledValue) 
                VALUES 
                (@Name, @Price, @ReceivedUnits, @ShippedUnits, @SpoiledUnits, 
                 @ReceivedValue, @ShippedValue, @SpoiledValue)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    foreach (var part in parts)
                    {
                        using (SqlCommand command = new SqlCommand(insertPart, connection))
                        {
                            command.Parameters.AddWithValue("@Name", part.Name);
                            command.Parameters.AddWithValue("@Price", part.Price);
                            command.Parameters.AddWithValue("@ReceivedUnits", part.ReceivedUnits);
                            command.Parameters.AddWithValue("@ShippedUnits", part.ShippedUnits);
                            command.Parameters.AddWithValue("@SpoiledUnits", part.SpoiledUnits);
                            command.Parameters.AddWithValue("@ReceivedValue", part.ReceivedValue);
                            command.Parameters.AddWithValue("@ShippedValue", part.ShippedValue);
                            command.Parameters.AddWithValue("@SpoiledValue", part.SpoiledValue);
                            command.ExecuteNonQuery();
                        }
                    }
                    Console.WriteLine("Successfully inserted Parts into DB.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inserting Parts: {ex.Message}");
                }
            }
        }
        static void CreatePartsXml()
        {
            string filePath = "../../Part.xml";
            string selectParts = "SELECT PartId, Name FROM Sales.Part";

            using (XmlWriter writer = XmlWriter.Create(filePath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Parts");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(selectParts, connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                writer.WriteStartElement("Part");
                                writer.WriteElementString("PartId", reader["PartId"].ToString());
                                writer.WriteElementString("Name", reader["Name"].ToString());
                                writer.WriteEndElement();
                            }
                            reader.Close();
                        }
                        Console.WriteLine("Successfully created Part.xml.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error generating Part.xml: {ex.Message}");
                    }
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
        static List<Customer> GetCustomers(XDocument xmlDoc)
        {
            return xmlDoc.Descendants("Customer")
                .Select(c => new Customer
                {
                    CustomerId = (int)c.Element("CustomerId"),
                    FirstName = (string)c.Element("FirstName"),
                    LastName = (string)c.Element("LastName"),
                    City = (string)c.Element("City"),
                    State = (string)c.Element("State")
                }).ToList();
        }
        static List<Part> GetParts(XDocument xmlDoc)
        {
            return xmlDoc.Descendants("Part")
                .Select(c => new Part
                {
                    PartId = (int)c.Element("PartId"),
                    Name = (string)c.Element("Name"),
                    Price = decimal.Parse((string)c.Element("Price")),
                    ReceivedUnits = int.Parse((string)c.Element("ReceivedUnits")),
                    ShippedUnits = int.Parse((string)c.Element("ShippedUnits")),
                    SpoiledUnits = int.Parse((string)c.Element("SpoiledUnits")),
                    ReceivedValue = decimal.Parse((string)c.Element("ReceivedValue")),
                    ShippedValue = decimal.Parse((string)c.Element("ShippedValue")),
                    SpoiledValue = decimal.Parse((string)c.Element("SpoiledValue"))
                }).ToList();
        }
        static List<SalesOrder> GetSalesOrders(XDocument xmlDoc)
        {
            return xmlDoc.Descendants("SalesOrder")
                .Select(c => new SalesOrder
                {
                    SalesOrderNumber = (int)c.Element("SalesOrderNumber"),
                    CustomerId = (int)c.Element("CustomerId"),
                    OrderDate = DateTime.Parse((string)c.Element("OrderDate"))
                }).ToList();
        }
        static List<SalesOrderPart> GetSalesOrderParts(XDocument xmlDoc)
        {
            return xmlDoc.Descendants("SalesOrderPart")
                .Select(c => new SalesOrderPart
                {
                    SalesOrderNumber = (int)c.Element("SalesOrderNumber"),
                    PartId = (int)c.Element("PartId"),
                    Quantity = (int)c.Element("Quantity"),
                    UnitsShipped = (int)c.Element("UnitsShipped"),
                    UnitPrice = (decimal)c.Element("UnitPrice"),
                    UnitCost = (decimal)c.Element("UnitCost")
                }).ToList();
        }
        static void GetData()
        {
            XDocument xmlDoc = XDocument.Load("../../Project2.xml");
            customers = GetCustomers(xmlDoc);
            parts = GetParts(xmlDoc);
            salesOrders = GetSalesOrders(xmlDoc);
            salesOrderParts = GetSalesOrderParts(xmlDoc);
            var salesOrdersLookup = salesOrders.ToLookup(so => so.CustomerId);
            var salesOrderPartsLookup = salesOrderParts.ToLookup(sop => sop.SalesOrderNumber);
            foreach (var customer in customers)
            {
                customer.SalesOrders = salesOrders.Where(so => so.CustomerId == customer.CustomerId).ToList();
                //customer.SalesOrders = salesOrdersLookup[customer.CustomerId].ToList();
            }
            foreach (var salesOrder in salesOrders)
            {
                salesOrder.SalesOrderParts = salesOrderPartsLookup[salesOrder.SalesOrderNumber].ToList();
                salesOrder.Customer = customers.First(c => c.CustomerId == salesOrder.CustomerId);
            }
            foreach (var part in parts)
            {
                part.SalesOrderParts = salesOrderParts.Where(sop => sop.PartId == part.PartId).ToList();
            }
            foreach (var sop in salesOrderParts)
            {
                sop.Part = parts.First(p => p.PartId == sop.PartId);
                sop.SalesOrder = salesOrders.First(so => so.SalesOrderNumber == sop.SalesOrderNumber);
            }
        }
    }
}


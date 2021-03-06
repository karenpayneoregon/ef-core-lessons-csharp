
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using NorthWindDataProviderLibrary.Models;

namespace NorthWindDataProviderLibrary.Classes
{
    public class SqlOperations
    {
        /// <summary>
        /// SQL-Server name
        /// </summary>
        public static string Server { get; set; }
        /// <summary>
        /// Database in Server
        /// </summary>
        public static string Database { get; set; }

        public static (bool, Exception, List<Customers>) GetCustomers()
        {
            var customers = new List<Customers>();

            try
            {
                using var cn = new SqlConnection($"Server={Server};Database={Database};Integrated Security=true");
                using var cmd = new SqlCommand("SELECT CustomerIdentifier,CompanyName FROM dbo.Customers;", cn);

                cn.Open();

                var reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    customers.Add(new Customers()
                    {
                        CustomerIdentifier = reader.GetInt32(0), 
                        CompanyName = reader.GetString(1)
                    });
                }
                
                return (true, null, customers);
                
            }
            catch (Exception exception)
            {
                return (true, exception, null);
            }

        }

        /// <summary>
        /// Demonstrates how developers not using EF Core read data from a database.
        /// * Requires more work than EF Core
        /// * String concatenation i not good (perhaps a Stored procedure, EF Core does SP's)
        /// </summary>
        /// <param name="phoneType"></param>
        /// <param name="contactType"></param>
        /// <returns></returns>
        public static (bool, Exception, List<CustomerItem>) GetCustomersJoinedTuple(int phoneType = 3, int contactType = 7)
        {
            var customers = new List<CustomerItem>();

            var selectStatement =
                "SELECT " + 
                    "C.CustomerIdentifier, C.CompanyName, C.City, C.PostalCode, Contacts.ContactId, " + 
                    "Countries.CountryIdentifier, Countries.Name AS Country, C.Phone, " + 
                    "Devices.PhoneTypeIdentifier, Devices.PhoneNumber, C.ModifiedDate " + 
                "FROM Customers AS C " + 
                    "INNER JOIN ContactType AS CT ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier " + 
                    "INNER JOIN Countries ON C.CountryIdentifier = Countries.CountryIdentifier " + 
                    "INNER JOIN Contacts ON C.ContactId = Contacts.ContactId " + 
                    "INNER JOIN ContactDevices AS Devices ON Contacts.ContactId = Devices.ContactId " +
                "WHERE " + 
                    "Devices.PhoneTypeIdentifier = @PhoneType AND C.ContactTypeIdentifier = @ContactType;";


            try
            {
                using var cn = new SqlConnection($"Server={Server};Database={Database};Integrated Security=true");
                using var cmd = new SqlCommand(selectStatement, cn);

                cmd.Parameters.Add("@PhoneType", SqlDbType.Int).Value = phoneType;
                cmd.Parameters.Add("@ContactType", SqlDbType.Int).Value = contactType;

                cn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    customers.Add(new CustomerItem()
                    {
                        CustomerIdentifier = reader.GetInt32(0), 
                        CompanyName = reader.GetString(1)
                    });
                }

                return (true, null, customers);

            }
            catch (Exception exception)
            {
                return (true, exception, null);
            }

        }
    }
}

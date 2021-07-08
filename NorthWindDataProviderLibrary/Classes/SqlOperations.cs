
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    }
}

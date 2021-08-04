using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthWindCoreLibrary.Models;

namespace NorthWindCoreUnitTest_InMemory.DataProvider
{
    /// <summary>
    /// Provides code to validate EF Core lambda/LINQ queries
    /// </summary>
    public class SqlOperations
    {
        public static  string ConnectionString = 
            "Data Source=.\\SQLEXPRESS;Initial Catalog=NorthWind2020;Integrated Security=True";

        public static Customers GetCustomers(int identifier)
        {
            Customers customer = new Customers();

            /*
             * Query to match EF Core Lambda statement.
             * No need for a formal parameter as this is used for a unit test.
             */
            var selectStatement = File.ReadAllText(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQL_Queries", "SingleCustomerByCompanyName.sql"))
                .Replace("@CustomerIdentifier", identifier.ToString());

            using var cn = new SqlConnection() { ConnectionString = ConnectionString };
            using var cmd = new SqlCommand() { Connection = cn, CommandText = selectStatement };
            
            
            
            return customer;
        }
    }
}

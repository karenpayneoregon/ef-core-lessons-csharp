using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWindCoreLibrary.Classes.North.Classes;
using NorthWindCoreLibrary.Data;
using NorthWindCoreLibrary.Models;
using NorthWindCoreLibrary.Projections;

namespace NorthWindCoreLibrary.Classes
{
    public class CustomersOperations
    {
        public static async Task<List<CustomerItem>> GetCustomersWithProjectionAsync()
        {

            return await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();
                return await context.Customers
                    .Select(CustomerItem.Projection)
                    .ToListAsync();
            });
        }
        public static CustomerEntity CustomerByIdentifier(int identifier)
        {
            using var context = new NorthwindContext();
            return context.Customers.Select(Customers.Projection)
                .FirstOrDefault(custEntity => custEntity.CustomerIdentifier == identifier);
        }
    }
}

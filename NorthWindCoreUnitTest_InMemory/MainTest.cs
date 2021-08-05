using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreLibrary.Classes.Helpers;
using NorthWindCoreLibrary.Data;
using NorthWindCoreLibrary.Models;
using NorthWindCoreUnitTest_InMemory.Base;
using NorthWindCoreUnitTest_InMemory.DataProvider;

namespace NorthWindCoreUnitTest_InMemory
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        /// <summary>
        /// Generate json files for in memory testing.
        /// Should only run this once then again any time
        /// one of the models changes
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        //[Ignore]
        [TestTraits(Trait.JsonGeneration)]
        public async Task CreateJsonFilesTask()
        {
            await SerializeModelsToJson();
        }
        
        /// <summary>
        /// Mockup for adding a single <see cref="Customers"/>
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.InMemoryTesting_CRUD)]
        public void AddCustomerTest()
        {
            Context.Entry(SingleContact).State = EntityState.Added;

            var customer = new Customers()
            {
                CompanyName = "Karen's coffee shop",
                Contact = SingleContact,
                CountryIdentifier = 20, 
                CountryIdentifierNavigation = new Countries() { Name = "USA" }
            };

            Context.Entry(customer).State = EntityState.Added;

            var saveChangesCount = Context.SaveChanges();

            Assert.IsTrue(saveChangesCount == 2,
                "Expect one customer and one contact to be added.");

        }

        [TestMethod]
        public void LoadingRelations()
        {
            int customerIdentifier = 3;
            
            var expected = SqlOperations.GetCustomers(customerIdentifier);
            

            var singleCustomer = Context.Customers
                .Include(customer => customer.CountryIdentifierNavigation)
                .Include(customer => customer.Contact)
                .ThenInclude(contact => contact.ContactDevices)
                .FirstOrDefault(customer => customer.CustomerIdentifier == customerIdentifier);

            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(singleCustomer.CompanyName, expected.CompanyName);
            Assert.AreEqual(singleCustomer.CountryIdentifierNavigation.Name, expected.Country);
            Assert.AreEqual(singleCustomer.Contact.FirstName, expected.FirstName);
            Assert.AreEqual(singleCustomer.Contact.LastName, expected.LastName);
            
            Assert.AreEqual(singleCustomer.Contact.ContactDevices.FirstOrDefault().PhoneNumber, expected.ContactPhoneNumber);

        }

        [TestMethod]
        public void CustomerCustomSort_City()
        {
            
            
            List<Customers> customersList = Context.Customers
                .Include(customer => customer.CountryIdentifierNavigation)
                .Include(customer => customer.Contact)
                .ThenInclude(contact => contact.ContactDevices)
                .ThenInclude(x => x.PhoneTypeIdentifierNavigation)
                .ToList().SortByPropertyName("CompanyName", SortDirection.Descending);

            Assert.IsTrue(customersList.FirstOrDefault().City == "Warszawa");
            Assert.IsTrue(customersList.LastOrDefault().City == "Berlin");
            

        }

        /// <summary>
        /// Demonstrates obtaining the query generated by EF Core using non in-memory DbContext
        /// as <seealso cref="EntityFrameworkQueryableExtensions.ToQueryString"/> does not support This extension
        ///
        /// Entity Framework Core can reveal a query by setting up logging in a DbContext which means each LINQ
        /// statement will write it's query to the desired output e.g. log file (normally for production) or
        /// to the console (usually for debugging).
        ///
        /// So ToQueryString is something to use one a LINQ statement is not providing proper results which can
        /// happen with improper joins and/or a bad database design.
        ///
        /// Notes
        ///  - ToQueryString works without actually making a call to a database
        ///  - ToQueryString is new, there may be some spots where it does not work as intend
        /// </summary>
        [TestMethod]
        public void GetQueryString()
        {
            using var context = new NorthwindContext();
            var query = context.Customers
                .Include(customer => customer.CountryIdentifierNavigation)
                .Include(customer => customer.Contact)
                .ThenInclude(contact => contact.ContactDevices)
                .ThenInclude(x => x.PhoneTypeIdentifierNavigation).ToQueryString();
            
            Debug.WriteLine(query);
            
        }
    }
}

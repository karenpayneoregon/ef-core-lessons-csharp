using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        [Ignore]
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
                .ThenInclude(x => x.ContactDevices)
                .FirstOrDefault(customer => customer.CustomerIdentifier == customerIdentifier);

            Assert.AreEqual(singleCustomer.CompanyName, expected.CompanyName);
            Assert.AreEqual(singleCustomer.CountryIdentifierNavigation.Name, expected.Country);
            Assert.AreEqual(singleCustomer.Contact.FirstName, expected.FirstName);
            Assert.AreEqual(singleCustomer.Contact.LastName, expected.LastName);
            Assert.AreEqual(singleCustomer.Contact.ContactDevices.FirstOrDefault().PhoneNumber, expected.ContactPhoneNumber);

        }

    }
}

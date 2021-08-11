using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreLibrary.Classes;
using NorthWindCoreLibrary.Classes.North.Classes;
using NorthWindCoreLibrary.Data;
using NorthWindCoreLibrary.Models;
using NorthWindCoreLibrary.Projections;
using NorthWindCoreUnitTest.Base;
using NorthWindCoreUnitTest.Classes;

namespace NorthWindCoreUnitTest
{
    /// <summary>
    /// Purpose, to show difference between EF Core and data provider access
    /// </summary>
    [TestClass]
    public partial class CustomersTest : TestBase
    {
        /// <summary>
        /// 
        /// </summary>
        
        [TestMethod]
        [TestTraits(Trait.EfCoreCustomersSelect)]
        public void CustomerCount()
        {
            using var context = new NorthwindContext();
            
            var customers = context.Customers.ToList();
            Assert.IsTrue(customers.Count == 91);
        }

        [TestMethod]
        [TestTraits(Trait.EfCoreCustomersSelect)]
        public async Task CustomersProject()
        {
            var customers = await CustomersOperations.GetCustomersWithProjectionAsync();

            string firstName = customers
                .FirstOrDefault(cust => cust.FirstName == "Maria").FirstName;
            
            Assert.IsTrue(firstName == "Maria");
            
        }

        #region Positive and negative test

        [TestMethod]
        [TestTraits(Trait.EfCoreCustomersSelect)]
        public void SingleCustomerByIdentifierGood()
        {
            int customerIdentifier = 1;
            CustomerEntity customer = CustomersOperations.CustomerByIdentifier(customerIdentifier);

            Assert.IsNotNull(customer);
            Assert.IsTrue(customer.CompanyName == "Alfreds Futterkiste");
        }
        [TestMethod]
        [TestTraits(Trait.EfCoreCustomersSelect)]
        public void SingleCustomerByIdentifierBad()
        {
            int customerIdentifier = 134;
            CustomerEntity customer = CustomersOperations.CustomerByIdentifier(customerIdentifier);
            Assert.IsNull(customer);
        }

        #endregion

        /// <summary>
        /// Uses <see cref="Task.WhenAll"/> to create a task that will complete when all of the supplied tasks have completed.
        /// In this case we are obtaining the same data although one is unsorted and the other sorted
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WhenAll()
        {
            Task<List<CustomerItem>> customersTask1 = CustomersOperations.GetCustomersWithProjectionAsync();
            Task<List<CustomerItemSort>> customersTask2 = CustomersOperations.GetCustomersWithProjectionSortAsync();
            await Task.WhenAll(customersTask1, customersTask2);

            List<CustomerItem> test1 = customersTask1.Result;
            List<CustomerItemSort> test2 = customersTask2.Result;

            Assert.AreEqual(customersTask1.Result.Count, 91);
            Assert.AreEqual(customersTask2.Result.Count, 91);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreLibrary.Classes;
using NorthWindCoreLibrary.Classes.North.Classes;
using NorthWindCoreLibrary.Data;
using NorthWindCoreLibrary.Models;
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
            var customers = 
                await CustomersOperations.GetCustomersWithProjectionAsync();

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


    }
}

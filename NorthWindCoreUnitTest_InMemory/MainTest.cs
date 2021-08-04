using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreLibrary.Models;
using NorthWindCoreUnitTest_InMemory.Base;

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
                CountryIdentifier = 20
            };

            Context.Entry(customer).State = EntityState.Added;

            var saveChangesCount = Context.SaveChanges();

            Assert.IsTrue(saveChangesCount == 2,
                "Expect one customer and one contact to be added.");

        }
    }
}

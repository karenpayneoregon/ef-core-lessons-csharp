using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreLibrary.Classes;
using NorthWindCoreLibrary.Data;
using NorthWindCoreUnitTest.Base;

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

    }
}

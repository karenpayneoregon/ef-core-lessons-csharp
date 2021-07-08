using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreLibrary.Classes;
using NorthWindCoreLibrary.Classes.North.Classes;
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

        [TestMethod]
        public void GroupByEmployeeIdentifierGetHighCountInOrders()
        {
            using var context = new NorthwindContext();
            var employeeList = context.Orders.Where(x => x.EmployeeId != null).Select(x => x.Employee).ToList();

            var employee = employeeList
                // group on EmployeeId
                .GroupBy(e => e.EmployeeId)
                // reverse order it on count
                .OrderByDescending(g => g.Count())
                // select the first
                .FirstOrDefault();

            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/default-values
            if (employee != default)
                Debug.WriteLine("Value: {0} employeeid: {1}", employee.Count(), employee.Key);



        }

    }
}

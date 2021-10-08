using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreLibrary.Classes;
using NorthWindCoreLibrary.Models;
using NorthWindCoreUnitTest.Base;
using NorthWindCoreUnitTest.Classes;

namespace NorthWindCoreUnitTest
{
    [TestClass]
    public partial class OrderTests
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// SELECT
        ///     EmployeeID,
        ///     COUNT(EmployeeID) AS Counter
        /// FROM
        ///     NorthWind2020.dbo.Orders
        /// GROUP BY
        ///     EmployeeID
        /// ORDER BY
        ///     Counter DESC
        /// </remarks>
        [TestMethod]
        [TestTraits(Trait.GroupingEntityFramework)]
        public async Task GroupByEmployeeIdentifierGetHighCountInOrders()
        {

            List<Employees> employeeList = await OrderOperations.GetEmployeesTask();
            IGrouping<int, Employees> employee = OrderOperations.EmployeeMostOrders(employeeList);

            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/default-values
            Assert.IsTrue(employee != default);

            Debug.WriteLine($"Order count: {employee.Count()} employee id: {employee.Key}");

            SqlOperations.Server = ".\\SQLEXPRESS";
            SqlOperations.Database = "NorthWind2020";

            //    |   |
            //    V   V    <- Discards
            var (_, _, dictionary) = SqlOperations.EmployeeMostOrders();

            Assert.AreEqual(employee.Count(), dictionary.FirstOrDefault().Value);

        }

        [TestMethod]
        [TestTraits(Trait.GroupingEntityFramework)]
        public async Task GroupList()
        {
            List<Employees> employeeList = await OrderOperations.GetEmployeesTask();

            var results = employeeList.GroupBy(employee => employee.EmployeeId)
                .Select(grouping => new EmpItem
                {
                    Id = grouping.Key, Count = grouping.Count(),
                    Employee = grouping.FirstOrDefault()
                })
                .OrderByDescending(empItem => empItem.Count)
                .ToList();


            foreach (EmpItem item in results)
            {
                Debug.WriteLine($"{item.Count,4:D3} {item.Employee.EmployeeId,4:D3} {item.Employee.LastName}");
            }

        }

        /// <summary>
        /// Demonstrates getting model definitions
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Cool)]
        public void ShowModel()
        {
            using var context = new NorthWindCoreLibrary.Data.NorthwindContext();
            var test = context.Model.ToDebugString(0);
            Debug.WriteLine(test);
        }

    }
}

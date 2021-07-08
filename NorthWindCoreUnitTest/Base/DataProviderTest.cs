using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindDataProviderLibrary.Classes;

namespace NorthWindCoreUnitTest
{
    public partial class DataProviderTest
    {
        [TestInitialize]
        public void Initialization()
        {
            if (TestContext.TestName == nameof(ReadCustomers) || TestContext.TestName == nameof(ReadCustomersWithJoins))
            {
                SqlOperations.Server = ".\\SQLEXPRESS";
                SqlOperations.Database = "NorthWind2020";
            }
        }
        
        /// <summary>
        /// Perform any initialize for the class
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }
    }
}

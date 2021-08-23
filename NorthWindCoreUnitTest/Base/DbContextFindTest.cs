using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindDataProviderLibrary.Classes;

// ReSharper disable once CheckNamespace
namespace NorthWindCoreUnitTest
{
    public partial class DbContextFindTest
    {
        [TestInitialize]
        public void Initialization()
        {
            if (TestContext.TestName == nameof(FindAllCategories) || TestContext.TestName == nameof(FindAllProducts))
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

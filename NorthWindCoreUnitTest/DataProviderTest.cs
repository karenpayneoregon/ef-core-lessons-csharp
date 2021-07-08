using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreUnitTest.Base;
using NorthWindDataProviderLibrary.Classes;

namespace NorthWindCoreUnitTest
{
    [TestClass]
    public partial class DataProviderTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.DataProviderCustomersSelect)]
        public void ReadCustomers()
        {


            var (success, exception, customersList) = SqlOperations.GetCustomers();
            Assert.IsTrue(success);
            Assert.IsTrue(customersList.Count == 91);
            
        }
        
    }
}

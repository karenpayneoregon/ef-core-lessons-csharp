using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreUnitTest.Base;
using NorthWindDataProviderLibrary.Classes;
using NorthWindDataProviderLibrary.Models;

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

        #region Demonstration only

        /// <summary>
        /// Demonstrates hand coding joins which return a list of <see cref="CustomerItem"/>
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.DataProviderCustomersSelect)]
        public void ReadCustomersWithJoins()
        {
            /*
             * The returning tuple
             * - success indicated the operation completed correctly
             * - _ is known as a discard meaning no intent to use the returning Exception
             * - customersList is the returning data of type CustomerItem list
             */
            var (success, _, customersList) = SqlOperations.GetCustomersJoinedTuple();
            Assert.IsTrue(success);
            Assert.IsTrue(customersList.Count == 16);
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ConfigurationHelper1.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NorthWindCoreUnitTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ConfigHelper1Test
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Tester1()
        {
            using var context = new NorthContext();
            var data = context.Categories.ToList();
            Assert.IsTrue(data.Count == 9);
        }

    }

}
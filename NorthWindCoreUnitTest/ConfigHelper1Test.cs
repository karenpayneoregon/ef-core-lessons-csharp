﻿using System;
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
using NorthWindCoreUnitTest.Base;

namespace NorthWindCoreUnitTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ConfigHelper1Test
    {
        /// <summary>
        /// Test read connection string from environment variable
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.EFConfiguration)]
        public void TesterConnection()
        {
            using var context = new NorthContext();
            var data = context.Categories.ToList();
            Assert.IsTrue(data.Count == 9);
        }

    }

}
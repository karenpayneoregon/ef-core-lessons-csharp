using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreLibrary.Classes;
using NorthWindCoreLibrary.Projections;
using NorthWindCoreUnitTest.Base;

namespace NorthWindCoreUnitTest
{
    [TestClass]
    public partial class ProductsTest : TestBase
    {
        
        /// <summary>
        /// Example for a complex group-by 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestTraits(Trait.GroupingEntityFramework)]
        public async Task GroupProductByCategoryThenByProduct()
        {
            List<Product> result = await ProductsOperations.GetProductsWithProjectionGroupByCategory();
            var returnCount = result.Count;
            var products = ProductsOperations.ReadProductsFromJsonFile("products.json");

            Assert.IsTrue(returnCount == products.Count);
            
        }
        [TestMethod]
        [TestTraits(Trait.GroupingEntityFramework)]
        public async Task GroupProductByCategoryThenByProductOrderByCategoryNameTask()
        {
            List<Product> result = await ProductsOperations.GetProductsWithProjectionGroupByCategory();

            StringBuilder sb = new StringBuilder();

            var ordered = result.OrderBy(product => product.CategoryId).ToList();

            foreach (var product in ordered)
            {
                sb.AppendLine($"{product.CategoryName,-20}{product.ProductName}");
            }

            await File.WriteAllTextAsync(ProductOrderByCategoryFile, sb.ToString());
        }

        [TestMethod]
        public async Task OrderedByCategoryNameTask()
        {
            StringBuilder sb = new StringBuilder();
            List<Product> ordered = await ProductsOperations.GetProductsWithProjectionGroupByCategoryOrderedTask();
            foreach (var product in ordered)
            {
                sb.AppendLine($"{product.CategoryName,-20}{product.ProductName}");
            }

            await File.WriteAllTextAsync(ProductOrderByCategoryFile, sb.ToString());
        }


    }
}

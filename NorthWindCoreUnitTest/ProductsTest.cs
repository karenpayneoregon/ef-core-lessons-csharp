using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreLibrary.Classes;
using NorthWindCoreLibrary.Data;
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
        [TestTraits(Trait.EFCore_OrderByProducts)]
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

        /// <summary>
        /// The DbSet&lt;<see cref="EntityEntry"/>>.Local provides access to the entity
        /// objects that are currently being tracked by the context and have not been marked as Deleted.
        /// It keeps track of entities whose entity state is added, modified and unchanged.
        /// </summary>
        /// <returns></returns>>
        [TestMethod]
        [TestTraits(Trait.EFCore_Products_Local)]
        public void LocalDataTest()
        {
            using var context = new NorthwindContext();

            context.Categories.Load();

            var category1 = context.Products.Find(1);
            context.Products.Remove(category1);


            var category2 = context.Categories.Find(2);
            category2.CategoryName = "Condiments-1";

            Debug.WriteLine("In Local: ");
            foreach (var category in context.Categories.Local)
            {
                Debug.WriteLine($"Found {category.CategoryId}: {category.CategoryName} with state {context.Entry(category).State}");
            }


            Assert.IsTrue(context.Entry(category1).State == EntityState.Deleted);

            Debug.WriteLine("------------------------");

            // Get all students from db.
            Debug.WriteLine("\nIn DbSet query: ");
            foreach (var category in context.Categories)
            {
                Debug.WriteLine($"Found {category.CategoryId}: {category.CategoryName} with state {context.Entry(category).State}");
            }
        }

        [TestMethod]
        [TestTraits(Trait.EFCore_Products_Local)]
        public void LocalToBindingList()
        {
            using var context = new NorthwindContext();

            context.Categories.Load();

            var bindingList = context.Categories.Local.ToBindingList();

        }


    }
}

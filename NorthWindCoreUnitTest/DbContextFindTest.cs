using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthWindCoreLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreLibrary.Classes;
using NorthWindCoreLibrary.Data;
using NorthWindCoreLibrary.LanguageExtensions;
using NorthWindCoreUnitTest.Base;
using NorthWindDataProviderLibrary.Classes;

namespace NorthWindCoreUnitTest
{
    /// <summary>
    /// 
    /// Notes
    ///    Random records is not so great for validation but had a moment and rolled with it :-)
    /// </summary>
    /// <remarks>
    /// Microsoft TechNet: Entity Framework Core Find all by primary key (C#)
    /// https://social.technet.microsoft.com/wiki/contents/articles/53841.entity-framework-core-find-all-by-primary-key-c.aspx
    /// </remarks>
    [TestClass]
    public partial class DbContextFindTest : TestBase
    {
        /// <summary>
        /// Rather than find one record using .FindAsync,
        /// FindAllAsync will get one to x by an array of object containing valid primary keys
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestTraits(Trait.EFCoreFind)]
        public async Task FindAllCategories()
        {
            
            int count = 4;
            object[] keyValues = SqlOperations.CategoryIdentifierList(count);
            await using var context = new NorthwindContext();

            var result = await context.FindAllAsync<Categories>(keyValues);
            Assert.AreEqual(result.Length, count);

        }
        [TestMethod]
        [TestTraits(Trait.EFCoreFind)]
        public async Task FindAllProducts()
        {

            int count = 4;
            object[] keyValues = SqlOperations.ProductIdentifierList(count);
            await using var context = new NorthwindContext();

            var result = await context.FindAllAsync<Products>(keyValues);
            Assert.AreEqual(result.Length, count);

        }

        /// <summary>
        /// Select random products
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task RandomProducts()
        {
            await using var context = new NorthwindContext();

            for (int index = 0; index < 4; index++)
            {

                foreach (var product in context.Products.OrderBy(r => Guid.NewGuid()).Take(5))
                {
                    Debug.WriteLine(product);
                }
                
                Debug.WriteLine("");
                
            }

            
        }

    }



}

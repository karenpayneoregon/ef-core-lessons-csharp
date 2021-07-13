using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreLibrary.Classes;
using NorthWindCoreLibrary.Data;
using NorthWindCoreLibrary.Projections;
using NorthWindCoreUnitTest.Base;

namespace NorthWindCoreUnitTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ContactTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.EFCoreContactSelect)]
        public void Projection()
        {
            using var context = new NorthwindContext();
            var contacts = context.Contacts.Select(ContactItem.Projection).ToList();
            Console.WriteLine();
        }

        [TestMethod]
        [TestTraits(Trait.GroupingEntityFramework)]
        public async Task GroupByContactType()
        {

            var test = await ContactOperations.ContactsGroupedByTitleAsync();

            foreach (var contactsGrouped in test)
            {
                Debug.WriteLine($"{contactsGrouped.FirstOrDefault().ContactTitle} - {contactsGrouped.Count()}");
                
                foreach (var contactItem in contactsGrouped.OrderBy(item => item.LastName))
                {
                    Debug.WriteLine($"\t{contactItem.FullName}");
                }
            }
        }
    }

}
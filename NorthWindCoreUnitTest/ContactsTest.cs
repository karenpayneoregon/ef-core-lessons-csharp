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

        /// <summary>
        /// Will run as the first test to warmup EF Core, expect 2 to 3 seconds.
        /// Each call there after will be milliseconds
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Warming)]
        public void A_Warmup()
        {
            ContactOperations.Warmup();
        }

        /// <summary>
        /// Code sample to get all contacts with their phone numbers where some
        /// will have 3,2 or 1 phone number
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.EFCoreContactSelect)]
        public void AllPhoneTest()
        {
            var contactsList = ContactOperations.Phones();
            foreach (var contact in contactsList)
            {
                Console.WriteLine($"{contact.ContactId}, {contact.FirstName} {contact.LastName} {contact.ContactDevices.Count}");
                foreach (var device in contact.ContactDevices)
                {
                    Console.WriteLine($"\t{device.PhoneTypeIdentifierNavigation.PhoneTypeDescription} - {device.PhoneNumber}");
                }
            }
        }

        [TestMethod]
        [TestTraits(Trait.EFCoreContactSelect)]
        public void SingleContactPhonesTest()
        {
            var contactIdentifier = 1;
            var contactsList = ContactOperations.Phones(contactIdentifier);
            Assert.IsTrue(contactsList.ContactDevices.Count == 3);
        }
    }

}
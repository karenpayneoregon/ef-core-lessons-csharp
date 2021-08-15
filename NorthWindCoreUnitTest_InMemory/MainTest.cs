using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreLibrary.Classes.Helpers;
using NorthWindCoreLibrary.Data;
using NorthWindCoreLibrary.LanguageExtensions;
using NorthWindCoreLibrary.Models;
using NorthWindCoreUnitTest_InMemory.Base;
using NorthWindCoreUnitTest_InMemory.DataProvider;
using NorthWindCoreUnitTest_InMemory.ValidationClasses;

namespace NorthWindCoreUnitTest_InMemory
{
    [TestClass]
    public partial class MainTest : TestBase
    {


        /// <summary>
        /// Mockup for adding a single <see cref="Customers"/>
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.CRUD)]
        public void AddSingleNewCustomer()
        {
            Context.Entry(SingleContact).State = EntityState.Added;

            var customer = new Customers()
            {
                CompanyName = "Karen's coffee shop",
                Contact = SingleContact,
                CountryIdentifier = 20, 
                CountryIdentifierNavigation = new Countries() { Name = "USA" }
            };

            Context.Entry(customer).State = EntityState.Added;

            var saveChangesCount = Context.SaveChanges();

            Assert.IsTrue(saveChangesCount == 2,
                "Expect one customer and one contact to be added.");

        }

        [TestMethod]
        [TestTraits(Trait.CRUD)]
        public void CustomersAddRange()
        {
            
            using var context = new NorthwindContext(dbContextRemoveOptions);

            context.Customers.AddRange(MockedInMemoryCustomers());
            context.Contacts.AddRange(MockedInMemoryContacts());
            
            context.SaveChanges();

            Assert.IsTrue(
                context.Customers.Count() == 20 && 
                context.Customers.ToList().All(x => x.Contact is not null)
            );

            var someCustomers = context.Customers.Take(3).ToList();
            
            context.Customers.RemoveRange(someCustomers);
            context.SaveChanges();
            Assert.AreEqual(context.Customers.Count(),17);


        }
        [TestMethod]
        [TestTraits(Trait.CRUD)]
        public void CustomersRemoveRange()
        {
            var someCustomers = Context.Customers.Take(3).ToList();
            Context.Customers.RemoveRange(someCustomers);
            Context.SaveChanges();
            Assert.AreEqual(Context.Customers.Count(),88);
        }

        /// <summary>
        /// Before EF Core 5 there was no method to filter related data, now we can use
        ///     .Where, .OrderBy, .OrderByDescending and .Take(x)
        /// </summary>
        /// <remarks>
        /// See
        ///
        /// Include uses Eager loading for related data
        /// https://docs.microsoft.com/en-us/ef/core/querying/related-data/eager
        /// 
        /// Filtered include
        /// https://docs.microsoft.com/en-us/ef/core/querying/related-data/eager#filtered-include
        /// 
        /// </remarks>
        [TestMethod]
        [TestTraits(Trait.Filtering)]
        public void FilteredInclude()
        {
            var germanyCountryIdentifier = 9;
            using var data = new NorthwindContext();
            
            var customersList = data.Customers.AsNoTracking()
                .Include(customer => customer.Orders)
                    .Where(x => x.CountryIdentifier == germanyCountryIdentifier)
                .ToList();
            
            Assert.IsTrue(customersList.Count == 11);
        }


        /// <summary>
        ///
        ///
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Relations)]
        public void LoadingRelations()
        {
            int customerIdentifier = 3;
            
            var expected = SqlOperations.GetCustomers(customerIdentifier);
            
            /*
             * Note 
             */
            var singleCustomer = Context.Customers
                .IncludeContactsDevicesCountry()
                .FirstOrDefault(customer => customer.CustomerIdentifier == customerIdentifier);


            Assert.AreEqual(singleCustomer.CompanyName, expected.CompanyName);
            Assert.AreEqual(singleCustomer.CountryIdentifierNavigation.Name, expected.Country);
            Assert.AreEqual(singleCustomer.Contact.FirstName, expected.FirstName);
            Assert.AreEqual(singleCustomer.Contact.LastName, expected.LastName);
            Assert.IsTrue(singleCustomer.Contact.ContactDevices.FirstOrDefault().PhoneTypeIdentifierNavigation.PhoneTypeDescription == "Office");
            Assert.AreEqual(singleCustomer.Contact.ContactDevices.FirstOrDefault().PhoneNumber, expected.ContactPhoneNumber);
            

        }

        [TestMethod]
        [TestTraits(Trait.Relations)]
        public void LoadingTheSinkRelations()
        {
            int customerIdentifier = 1;

            var singleCustomer = Context.Customers
                .IncludeContactsDevicesCountry()
                .FirstOrDefault(customer => customer.CustomerIdentifier == customerIdentifier);

            Debug.WriteLine($"{singleCustomer.Contact.FirstName} {singleCustomer.Contact.LastName}");
            foreach (var device in singleCustomer.Contact.ContactDevices)
            {
                Debug.WriteLine($"{GetPhoneType(device.PhoneTypeIdentifier.Value)} {device.Contact.LastName} {device.PhoneNumber}");
            }
        }





        [TestMethod]
        [TestTraits(Trait.CustomSorting)]
        public void CustomerCustomSort_City()
        {
            
            List<Customers> customersList = Context.Customers
                .Include(customer => customer.CountryIdentifierNavigation)
                .Include(customer => customer.Contact)
                .ThenInclude(contact => contact.ContactDevices)
                .ThenInclude(x => x.PhoneTypeIdentifierNavigation)
                .ToList()
                .SortByPropertyName("CompanyName", SortDirection.Descending);

            Assert.IsTrue(customersList.FirstOrDefault().City == "Warszawa");
            Assert.IsTrue(customersList.LastOrDefault().City == "Berlin");
            

        }

        /// <summary>
        /// Demonstrates obtaining the query generated by EF Core using non in-memory DbContext
        /// as <seealso cref="EntityFrameworkQueryableExtensions.ToQueryString"/> does not support This extension
        ///
        /// Entity Framework Core can reveal a query by setting up logging in a DbContext which means each LINQ
        /// statement will write it's query to the desired output e.g. log file (normally for production) or
        /// to the console (usually for debugging).
        ///
        /// So ToQueryString is something to use one a LINQ statement is not providing proper results which can
        /// happen with improper joins and/or a bad database design.
        ///
        /// Notes
        ///  - ToQueryString works without actually making a call to a database
        ///  - ToQueryString is new, there may be some spots where it does not work as intend
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.Utility)]
        public void GetQueryString()
        {
            using var context = new NorthwindContext();
            var query = context.Customers
                .Include(customer => customer.CountryIdentifierNavigation)
                .Include(customer => customer.Contact)
                .ThenInclude(contact => contact.ContactDevices)
                .ThenInclude(x => x.PhoneTypeIdentifierNavigation).ToQueryString();
            
            Debug.WriteLine(query);
            
        }

        [TestMethod]
        [TestTraits(Trait.CRUD)]
        public void RemoveSingleCustomer()
        {
            
            Assert.IsTrue(DeleteCustomer());
        }

        /// <summary>
        /// Find by primary key
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.AccessTrackedEntities)]
        public void FindByPrimaryKey()
        {
            var customer = Context.Customers.Find(3);
            Assert.IsTrue(customer.CompanyName == "Antonio Moreno Taquer�a");
        }

        /// <summary>
        /// Example for obtaining current and original values of a tracked entity
        ///
        /// Note CurrentValue and OriginalValue are get/setters
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.AccessTrackedEntities)]
        public void ContactOriginalCurrentValueCheck()
        {
            var firstName = "Karen";
            var changedFirstName = "Mary";

            // create a new Contact and save
            Contacts contacts = new Contacts()
            {
                FirstName = firstName
            };
            
            Contacts contact1 = new Contacts()
            {
                ContactId = 1, 
                FirstName = "Bick", 
                LastName = "VU"
            };


            Context.Add(contacts);
            Assert.IsTrue(Context.SaveChanges() == 1);

            // get current first name
            var currentFirstName = Context.Entry(SingleContact)
                .Property(contact => contact.FirstName).CurrentValue;

            Assert.AreEqual(currentFirstName, firstName);

            // change first name
            contacts.FirstName = changedFirstName;
            
            // validate first name changed
            Assert.AreEqual(contacts.FirstName, changedFirstName );

            // get original first name
            var originalFirstName = Context.Entry(SingleContact)
                .Property(contact => contact.FirstName).OriginalValue;

            // assert we got the original value
            Assert.IsTrue(originalFirstName == firstName);


            /*
             * Let's clone the  contact (without as Contacts clonedContact is type object but we know better)
             */
            var clonedContact = Context.Entry(contacts).GetDatabaseValues().ToObject() as Contacts;
            
            /*
             * In short set all properties of contact1 to contact object
             */
            Context.Entry(contacts).CurrentValues.SetValues(contact1);
            Assert.IsTrue(contacts.LastName == "VU");
            
            /*
             * Assert the clone contact last name is empty
             */
            Assert.IsNull(clonedContact.LastName);
        }

        /*
         * Karen - next code samples to work on
         * https://docs.microsoft.com/en-us/ef/core/change-tracking/entity-entries#using-changetrackerentries-to-access-all-tracked-entities
         */

        #region Working with live data, same can be done with in-memory


        [TestMethod]
        [TestTraits(Trait.AccessTrackedEntities)]
        public void FindAndLoadSingleCollection()
        {
            using var context = new NorthwindContext();

            var customer = context.Customers.Find(3);
            Assert.IsTrue(customer.CompanyName == "Antonio Moreno Taquer�a");

            Assert.IsTrue(customer.Orders.Count == 0);
            context.Entry(customer).Collection(e => e.Orders).Load();
            Assert.IsTrue(customer.Orders.Count > 0);

        }

        /// <summary>
        /// Demonstrates modifying a entry state
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.AccessTrackedEntities)]
        public void FindAndModifySingleEntry()
        {
            using var context = new NorthwindContext();

            var customer = context.Customers.Find(3);
            Assert.IsTrue(customer.CompanyName == "Antonio Moreno Taquer�a");
            Assert.IsTrue(context.Entry(customer).State == EntityState.Unchanged);

            customer.CompanyName = "ABC";
            Assert.IsTrue(context.Entry(customer).State == EntityState.Modified);
        }

        /// <summary>
        /// Example for a like condition for starts with and case insensitive.
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.LikeConditions)]
        public void LikeStartwWith()
        {
            Context.Customers.AddRange(MockedInMemoryCustomers());
            Context.SaveChanges();

            List<Customers> results = Context.Customers
                .Where(customer => EF.Functions.Like(customer.CompanyName, "an%"))
                .ToList();
            
            
            Assert.AreEqual(results.Count, 2);
            

        }

        [TestMethod]
        [TestTraits(Trait.LikeConditions)]
        public void LikeEndWith()
        {
            Context.Customers.AddRange(MockedInMemoryCustomers());
            Context.SaveChanges();

            List<Customers> results = Context.Customers
                .Where(customer => EF.Functions.Like(customer.CompanyName, "%S.A."))
                .ToList();

            Assert.AreEqual(results.Count, 1);
        }

        [TestMethod]
        [TestTraits(Trait.LikeConditions)]
        public void LikeContains()
        {
            Context.Customers.AddRange(MockedInMemoryCustomers());
            Context.SaveChanges();

            List<Customers> results = Context.Customers
                .Where(customer => EF.Functions.Like(customer.CompanyName, "%Comidas%"))
                .ToList();

            Assert.AreEqual(results.Count, 2);


        }

        [TestMethod]
        [TestTraits(Trait.AccessTrackedEntities)]
        public void ChangeCurrentValueByType()
        {
            var expectedDate = new DateTime(2021, 7, 4);
            using var context = new NorthwindContext();

            /*
             * Get Customer by primary key
             */
            var customer = context.Customers.Find(3);

            /*
             * Rather than directly setting ModifiedDate we look for it via type
             * Note: DateTime will fail as ModifiedDate is nullable
             */
            foreach (var propertyEntry in context.Entry(customer).Properties)
            {

                if (propertyEntry.Metadata.ClrType == typeof(DateTime?))
                {
                    propertyEntry.CurrentValue = expectedDate;
                }
            }

            // Assert
            Assert.AreEqual(customer.ModifiedDate, expectedDate);


            /*
             * Get original values from the database
             */
            var originalCustomer = context.Customers.AsNoTracking()
                .FirstOrDefault(cust => cust.CustomerIdentifier == customer.CustomerIdentifier);

            /*
             * Revert to ModifiedDate original value
             */
            customer.ModifiedDate = originalCustomer.ModifiedDate;

            // Assert
            Assert.AreNotEqual(customer.ModifiedDate, expectedDate);
            

        }

        #endregion

        #region Basic fluent validation 

        /// <summary>
        /// No Assert required, on failure an exception is thrown
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.FluentValidation)]
        public void ValidateCompanyNameIsNull()
        {

            var singleCustomers = new Customers() { CompanyName = null };
            TestValidationResult<Customers> result = customersValidator.TestValidate(singleCustomers);
            result.ShouldHaveValidationErrorFor(customer => customer.CompanyName);
            result.ShouldHaveValidationErrorFor(customer => customer.ModifiedDate);

        }
        
        /// <summary>
        /// Inspect violations
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.FluentValidation)]
        public void ValidateCompanyNameIsNull_1()
        {

            var singleCustomers = new Customers() { CompanyName = null };


            ValidationResult results = customersValidator.Validate(singleCustomers);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine($"Property {failure.PropertyName} failed validation. Error was: {failure.ErrorMessage}");
                }
            }

        }

        /// <summary>
        /// No Assert required, on failure an exception is thrown
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.FluentValidation)]
        public void ValidateCompanyNameIsNotNull()
        {

            var singleCustomer = MockedInMemoryCustomers().FirstOrDefault();

            TestValidationResult<Customers> result = customersValidator1.TestValidate(singleCustomer);

            result.ShouldNotHaveAnyValidationErrors();

        }

        #endregion


    }
}

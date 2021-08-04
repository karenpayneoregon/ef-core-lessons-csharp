﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NorthWindCoreLibrary.Classes.Helpers;
using NorthWindCoreLibrary.Classes.North.Classes;
using NorthWindCoreLibrary.Data;
using NorthWindCoreLibrary.Models;


// ReSharper disable once CheckNamespace - do not change
namespace NorthWindCoreUnitTest_InMemory
{
    public partial class MainTest
    {
        /// <summary>
        /// Options for in-memory testing
        /// </summary>
        readonly DbContextOptions<NorthwindContext> dbContextOptions = new DbContextOptionsBuilder<NorthwindContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        /// <summary>
        /// Single instance of the <see cref="NorthWindContext"/> for in-memory context
        /// </summary>
        private NorthwindContext Context;

        /// <summary>
        /// Perform initialization before test runs using assertion on current test name.
        /// </summary>
        [TestInitialize]
        public void Initialization()
        {
            Context = new NorthwindContext(dbContextOptions);
            
            annihilationList = new List<object>();

            if (TestContext.TestName == nameof(LoadingRelations))
            {
                LoadJoinedData();
            }
        }

        /// <summary>
        /// Perform cleanup after test runs using assertion on current test name.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.TestName == nameof(CreateJsonFilesTask))
            {
                // TODO
            }
        }
        /// <summary>
        /// Perform any initialize for the class
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }

        /// <summary>
        /// Base instance of a <see cref="Contacts"/>
        /// </summary>
        public Contacts SingleContact => new()
        {
            FirstName = "Karen",
            LastName = "Payne",
            ContactTypeIdentifier = 1,
            ContactTypeIdentifierNavigation = new ContactType()
            {
                ContactTypeIdentifier = 1,
                ContactTitle = "Accounting Manager",
                Contacts = new List<Contacts>() { new()
                {
                    ContactId = 1, 
                    ContactTypeIdentifier = 1,
                    FirstName = "Bernardo",
                    LastName = "Batista"
                } },
            },
            ContactDevices = new List<ContactDevices>()
        };

        #region Json files generation

        private static readonly string customersJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Customers.json");
        private static readonly string contactTypeJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "ContactType.json");
        private static readonly string contactsJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Contacts.json");
        private static readonly string countriesJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Countries.json");
        private static readonly string contactDevicesJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "ContactDevices.json");

        public static async Task SerializeModelsToJson()
        {

            await using var context = new NorthwindContext();

            List<CustomerEntity> cust = await AllCustomersToJsonAsync();
            await File.WriteAllTextAsync(customersJsonFileName, JsonHelpers.Serialize<CustomerEntity>(cust));

            List<ContactType> contactTypes = context.ContactType.ToList();
            await File.WriteAllTextAsync(contactTypeJsonFileName, JsonHelpers.Serialize<ContactType>(contactTypes));

            List<Contacts> contacts = context.Contacts.ToList();
            await File.WriteAllTextAsync(contactsJsonFileName, JsonHelpers.Serialize<Contacts>(contacts));

            List<Countries> countriesList = context.Countries.ToList();
            await File.WriteAllTextAsync(countriesJsonFileName, JsonHelpers.Serialize<Countries>(countriesList));

            List<ContactDevices> contactDeviceList = context.ContactDevices.ToList();
            await File.WriteAllTextAsync(contactDevicesJsonFileName, JsonHelpers.Serialize<ContactDevices>(contactDeviceList));
        }

        public static async Task<List<CustomerEntity>> AllCustomersToJsonAsync()
        {

            return await Task.Run(async () =>
            {
                await using var context = new NorthwindContext();
                List<CustomerEntity> customerItemsList = await context.Customers
                    .Include(customer => customer.Contact)
                    .Select(Customers.Projection)
                    .ToListAsync();

                return customerItemsList.OrderBy((customer) => customer.CompanyName).ToList();
            });

        }
        private static List<Customers> CustomersRelationsData(
            out List<ContactType> contactTypeList, 
            out List<Contacts> contactList, 
            out List<Countries> countriesList, 
            out List<ContactDevices> contactDevicesList)
        {
            List<Customers> customersList = JsonConvert.DeserializeObject<List<Customers>>(File.ReadAllText(customersJsonFileName));
            contactTypeList = JsonConvert.DeserializeObject<List<ContactType>>(File.ReadAllText(contactTypeJsonFileName));
            contactList = JsonConvert.DeserializeObject<List<Contacts>>(File.ReadAllText(contactsJsonFileName));
            countriesList = JsonConvert.DeserializeObject<List<Countries>>(File.ReadAllText(countriesJsonFileName));
            contactDevicesList = JsonConvert.DeserializeObject<List<ContactDevices>>(File.ReadAllText(contactDevicesJsonFileName));


            return customersList;
        }

        public void LoadJoinedData()
        {
            Context = new NorthwindContext(dbContextOptions);

            
            var customersList = CustomersRelationsData(out var contactTypeList, out var contactList, out var countriesList, out var contactDevicesList);

            Context.Customers.AddRange(customersList!);
            Context.ContactType.AddRange(contactTypeList!);
            Context.Contacts.AddRange(contactList!);
            Context.Countries.AddRange(countriesList!);
            Context.ContactDevices.AddRange(contactDevicesList);
            var count = Context.SaveChanges();
            var tep = Context;
        }

        #endregion

        protected List<object> annihilationList;

        protected T AddSandboxEntity<T>(T sandboxEntity) where T : class
        {
            annihilationList.Add(sandboxEntity);

            return sandboxEntity;
        }
        protected IEnumerable<T> GetSandboxEntities<T>() => (annihilationList.Where(item => item.GetType() == typeof(T)).Select(item => (T)item));
    }
}

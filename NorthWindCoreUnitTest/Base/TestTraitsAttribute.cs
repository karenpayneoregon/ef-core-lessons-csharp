using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using NorthWindCoreLibrary.Models;

namespace NorthWindCoreUnitTest.Base
{
    public enum Trait
    {
        /// <summary>
        /// Data provider
        /// </summary>
        DataProviderCustomersSelect,
        /// <summary>
        /// EF Core select operations
        /// </summary>
        EfCoreCustomersSelect,
        /// <summary>
        /// EF Core projections
        /// </summary>
        GroupingEntityFramework,
        /// <summary>
        /// EF Core select operations for <see cref="Contacts"/>
        /// </summary>        
        EFCoreContactSelect,
        EFCore_OrderByProducts,
        /// <summary>
        /// Configuring EF Core for multiple environments
        /// </summary>
        EFConfiguration,
        /// <summary>
        /// Used to simulate warming up EF Core
        /// </summary>
        Warming,
        EFCore_Products_Local
    }
    /// <summary>
    /// Declarative class for using Trait enum about for traits on test method.
    /// </summary>
    public class TestTraitsAttribute : TestCategoryBaseAttribute
    {
        private readonly Trait[] _traits;

        public TestTraitsAttribute(params Trait[] traits)
        {
            _traits = traits;
        }

        public override IList<string> TestCategories => _traits.Select(trait => Enum.GetName(typeof(Trait), trait)).ToList();
    }

}
# About

![immg](assets/efcore_csharp.png)

This repository is for teaching using Microsoft Entity Framework Core with a modified version of Microsoft [NorthWind database](https://gist.github.com/karenpayneoregon/40a6e1158ff29819286a39b7f1ed1ae8).

First lesson walkthrough building the foundation for

- Reading data with and without related data with EF Core and SQL-Server data provider
- Using projections
- Unit testing
- Various [DbContext](https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-5.0) configurations
  - Including [ProxiesExtensions](https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.proxiesextensions?view=efcore-5.0) 
- Reverse Engineering a database using [EF Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools)
- Source control, [GitHub](https://github.com/) in Visual Studio


# Sample test methods

```csharp
namespace NorthWindCoreUnitTest
{
    [TestClass]
    public partial class CustomersTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.EfCoreCustomersSelect)]
        public void CustomerCount()
        {
            using var context = new NorthwindContext();
            
            var customers = context.Customers.ToList();
            Assert.IsTrue(customers.Count == 91);
        }
        [TestMethod]
        [TestTraits(Trait.EfCoreCustomersSelect)]
        public async Task CustomersProject()
        {
            var customers = 
                await CustomersOperations.GetCustomersWithProjectionAsync();

            string firstName = customers
                .FirstOrDefault(cust => cust.FirstName == "Maria").FirstName;
            
            Assert.IsTrue(firstName == "Maria");
        }
    }
}
```

# Future lessons

Will be done in branches in GitHub
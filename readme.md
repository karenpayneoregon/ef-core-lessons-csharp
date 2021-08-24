# About Part IV

![immg](assets/efcore_csharp.png)

In this branch

:heavy_check_mark: Grouping data.

Grouping is one of the most powerful capabilities of LINQ. The following examples show how to group data in various ways:

- By a single property.
- By the first letter of a string property.
- By a computed numeric range.
- By Boolean predicate or other expression.
- By a compound key.

## Lessons

Working with `NorthWind` database ([script](https://gist.github.com/karenpayneoregon/40a6e1158ff29819286a39b7f1ed1ae8) found here)

:heavy_check_mark: One property [GroupBy](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.groupby?view=net-5.0)

:heavy_check_mark: Complex multiple property [GroupBy](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.groupby?view=net-5.0) with [Projections](https://github.com/karenpayneoregon/ef-core-lessons-csharp/tree/Part5_InMemory/NorthWindCoreLibrary/Projections)

:heavy_check_mark: In-memory unit testing EF Core 5

| Note  |
| :--- |
| There are several different ways to perform testing, see [Testing code that uses EF Core](https://docs.microsoft.com/en-us/ef/core/testing/) |
| :warning: The EF in-memory database often behaves differently than relational databases. Only use the EF in-memory database after fully understanding the issues and trade-offs involved, as discussed in [Testing code that uses EF Core](https://docs.microsoft.com/en-us/ef/core/testing/).|


:heavy_check_mark: [FindAllAsync](https://github.com/karenpayneoregon/ef-core-lessons-csharp/blob/Part5_InMemory/NorthWindCoreLibrary/LanguageExtensions/EntityHelpers.cs#L20)

| Multiple key find  |
| :--- |
| To find a record by primary key, many developers will use .[Where](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where?view=net-5.0) or .[FirstOrDefault](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault?view=net-5.0). Better to use .[Find](https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.find?view=efcore-5.0) while to find multiple records [FindAllAsync](FindAllAsync) is a better choice. |
| **Find**</br> an entity with the given primary key values. If an entity with the given primary key values is being tracked by the context, then it is returned immediately without making a request to the database. Otherwise, a query is made to the database for an entity with the given primary key values and this entity, if found, is attached to the context and returned. If no entity is found, then null is returned. |

:heavy_check_mark: [Interceptors](https://docs.microsoft.com/en-us/ef/core/logging-events-diagnostics/interceptors)

| Interceptors  |
| :--- |
| Entity Framework Core (EF Core) interceptors enable interception, modification, and/or suppression of EF Core operations. This includes low-level database operations such as executing a command, as well as higher-level operations, such as calls to SaveChanges.|
| Interceptors are different from logging and diagnostics in that they allow modification or suppression of the operation being intercepted. Simple logging or Microsoft.Extensions.Logging are better choices for logging.|
| Interceptors are registered per DbContext instance when the context is configured. Use a diagnostic listener to get the same information but for all DbContext instances in the process.|
| |


## Notes

- There are many different types of group-by operations to consider, here are a few.
- The complex example is one that many developer struggle with which is why it's included.

:yellow_circle: Extra - DBContext connection via environment variable

## Unit testing

![img](assets/oops.png)
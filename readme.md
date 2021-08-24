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

:heavy_check_mark: One property GroupBy

:heavy_check_mark: Complex multiple property `GroupBy` with `Projections`

:heavy_check_mark: In-memory unit testing EF Core 5 (really part 6)

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
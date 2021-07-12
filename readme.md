# About Part IV

![immg](assets/efcore_csharp.png)

In this branch

:heavy_check_mark: Added EF Core Logging

When a LINQ/Lamdba query has issues, perhaps with the proper `join` or `runs slow` using logging can assist while in development mode.

# Required NuGet packages

- Microsoft.Extensions.Logging
- Microsoft.Extensions.Logging.Debug

# Log options

These are common logging to the Debug window or file. They can be modified to what is shown in the logs such as parameter information or no parameter information.

**See**: Microsoft docs [Overview of Logging and Interception](https://docs.microsoft.com/en-us/ef/core/logging-events-diagnostics/)

```csharp
/// <summary>
/// Log to file specified in <see cref="_logStream"/>
/// </summary>
/// <param name="optionsBuilder"></param>
private void LogQueryInfoToFile(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer(Helper.ConnectionString())
        .EnableSensitiveDataLogging()
        .LogTo(message => _logStream.WriteLine(message),
            LogLevel.Information,
            DbContextLoggerOptions.Category);
}
private static void NoLogging(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer(Helper.ConnectionString());
}
```
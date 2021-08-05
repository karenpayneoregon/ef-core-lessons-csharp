# About

TODO


## NuGet packages

- [Microsoft.EntityFrameworkCore.InMemory](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.InMemory/6.0.0-preview.6.21352.1) 
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)
 
# Build events

Open project properties, select Build Events, examine Post-build event command line.

```
if not exist $(TargetDir)\Json mkdir $(TargetDir)\Json
```

This ensures a folder named Json exists in the folder where binaries are located for placing Json files into for generating Json from models.

Since the files in the Json folder are under source control when cloning this solution they will be downloaded.


# Change tracking

## Accessing Tracked Entities

Microsoft docs: [There are four main APIs for accessing entities tracked by a DbContext](https://docs.microsoft.com/en-us/ef/core/change-tracking/entity-entries)

## Identity Resolution in EF Core

A DbContext can only track one entity instance with any given primary key value. This means multiple instances of an entity with the same key value must be resolved to a single instance. This is called "[identity resolution](https://docs.microsoft.com/en-us/ef/core/change-tracking/identity-resolution)". Identity resolution ensures Entity Framework Core (EF Core) is tracking a consistent graph with no ambiguities about the relationships or property values of the entities.

## Change Detection and Notifications

Each DbContext instance [tracks changes](https://docs.microsoft.com/en-us/ef/core/change-tracking/change-detection) made to entities.

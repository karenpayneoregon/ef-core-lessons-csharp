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
This library uses [Microsoft.Build](1) to load solution files and projects that are in the solution. It also loads the packages.config file, if any, using [NuGet.Core](2) and exposes the information of the packages it finds there. In addition it exposes file and directory information for projects and solutions.

[1]: https://www.nuget.org/packages/Microsoft.Build/
[2]: https://www.nuget.org/packages/NuGet.Core/


# Usage
## Using ServiceCollection
```C#
var collection = new ServiceCollection();
collection.AddVisualStudioFiles();
            
var provider = collection.BuildServiceProvider();

.....
var reader = provider.GetService<ISolutionReader>();
var solution = reader.ReadFromFile("....");
```

## Using SolutionReaderFactory
```C#
var reader = SolutionReaderFactory.Create();
var solution = reader.ReadFromFile("....");
```
[![Build Status](https://sandera.visualstudio.com/_apis/public/build/definitions/0c96c15e-785f-4837-abb8-27bbc6bb2a7e/17/badge)]()

This library uses [Microsoft.Build](1) to load solution files and projects that are in the solution. It also loads the packages.config file, if any, using [NuGet.Core](2) and exposes the information of the packages it finds there.

[1]: https://www.nuget.org/packages/Microsoft.Build/
[2]: https://www.nuget.org/packages/NuGet.Core/
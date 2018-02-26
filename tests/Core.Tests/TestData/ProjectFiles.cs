namespace VisualStudio.Files.Core.Tests.TestData
{
    internal static class ProjectFiles
    {
        internal static string Empty = "" +
             "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
             "<Project ToolsVersion=\"15.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
             "</Project>";

        internal static string WithSingleOutputPathWithVars = "" +
              "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
              "<Project ToolsVersion=\"15.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                  "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|x86' \">" +
                      "<SomeVar>x86d</SomeVar>" +
                      "<OutputPath>..\\..\\..\\bin\\$(SomeVar)\\</OutputPath>" +
                  "</PropertyGroup>" +
              "</Project>";
        
        internal static string WithSingleOutputPathWithoutVars = "" +
              "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
              "<Project ToolsVersion=\"15.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                  "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|x86' \">" +
                      "<OutputPath>..\\..\\..\\bin\\x86d\\</OutputPath>" +
                  "</PropertyGroup>" +
              "</Project>";
        
        internal static string WithMultipleOutputPathsWithVars = "" +
                                                      "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                                      "<Project ToolsVersion=\"15.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" +
                                                          "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|x86' \">" +
                                                              "<SomeVar>x86d</SomeVar>" +
                                                              "<OutputPath>..\\..\\..\\bin\\$(SomeVar)\\</OutputPath>" +
                                                          "</PropertyGroup>" +
                                                          "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|x86' \">" +
                                                              "<SomeVar>x86</SomeVar>" +
                                                              "<OutputPath>..\\..\\..\\bin\\$(SomeVar)\\</OutputPath>" +
                                                          "</PropertyGroup>" +
                                                          "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|x64' \">" +
                                                              "<SomeVar>x64d</SomeVar>" +
                                                              "<OutputPath>..\\..\\..\\bin\\$(SomeVar)\\</OutputPath>" +
                                                          "</PropertyGroup>" +
                                                              "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|x64' \">" +
                                                              "<SomeVar>x64</SomeVar>" +
                                                          "<OutputPath>..\\..\\..\\bin\\$(SomeVar)\\</OutputPath>" +
                                                          "</PropertyGroup>" +
                                                      "</Project>";
    }
}
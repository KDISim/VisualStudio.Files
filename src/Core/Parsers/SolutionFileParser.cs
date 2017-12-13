using System;
using System.Text.RegularExpressions;
using ISolutionFile = VisualStudio.Files.Core.Wrappers.ISolutionFile;
using SolutionFileWrapper = VisualStudio.Files.Core.Wrappers.SolutionFileWrapper;

namespace VisualStudio.Files.Core.Parsers
{
    internal class SolutionFileParser : ISolutionFileParser
    {
        // An example of a project line looks like this:
        //  Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "ClassLibrary1", "ClassLibrary1\ClassLibrary1.csproj", "{05A5AD00-71B5-4612-AF2F-9EA9121C4111}"
        private static readonly Lazy<Regex> ProjectLineRegex = new Lazy<Regex>(
            () => new Regex
                (
                "^" // Beginning of line
                + "Project\\(\"(?<PROJECTTYPEGUID>.*)\"\\)"
                + "\\s*=\\s*" // Any amount of whitespace plus "=" plus any amount of whitespace
                + "\"(?<PROJECTNAME>.*)\""
                + "\\s*,\\s*" // Any amount of whitespace plus "," plus any amount of whitespace
                + "\"(?<RELATIVEPATH>.*)\""
                + "\\s*,\\s*" // Any amount of whitespace plus "," plus any amount of whitespace
                + "\"(?<PROJECTGUID>.*)\""
                + "$", // End-of-line
                RegexOptions.Compiled
                )
        );
        
        public ISolutionFile Parse(string path)
        {
            return new SolutionFileWrapper(path);
        }
    }
}
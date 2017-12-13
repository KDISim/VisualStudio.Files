using System.IO;
using VisualStudio.Files.Abstractions;
using ISolutionFile = VisualStudio.Files.Core.Wrappers.ISolutionFile;

namespace VisualStudio.Files.Core.Factories
{
    internal interface ISolutionFactory
    {
        ISolution Create(ISolutionFile solutionFile, FileInfo solutionFileInfo);
    }
}
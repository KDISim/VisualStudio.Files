using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using VisualStudio.Files.Abstractions.RunSettings;

namespace VisualStudio.Files.Abstractions
{
    public interface IProject : IFileInfo
    {
        IEnumerable<IRunSettingsFile> RunSettings { get; }

        bool TryGetOutputDirectory(string configuration, string platform, out DirectoryInfo outputDirectory);
    }
}
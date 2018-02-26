using System.Collections.Generic;
using System.Xml.Linq;
using VisualStudio.Files.Abstractions.RunSettings;

namespace VisualStudio.Files.Abstractions
{
    public interface IProject : IFileInfo
    {
        IEnumerable<IRunSettingsFile> RunSettings { get; } 
        XElement XmlRoot { get; }
        
        IOutputPath GetOutputPath(string configuration, string platform);
    }
}
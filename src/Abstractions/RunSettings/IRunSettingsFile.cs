using System.Xml;
using System.Xml.Linq;

namespace VisualStudio.Files.Abstractions.RunSettings
{
    public interface IRunSettingsFile : IFileInfo
    {
        XElement XmlRoot { get; }
        
        INUnitSection NUnit { get; }
    }
}
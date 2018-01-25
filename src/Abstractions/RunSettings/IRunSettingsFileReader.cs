using VisualStudio.Files.Abstractions.RunSettings;

namespace VisualStudio.Files.Core.Readers
{
    public interface IRunSettingsFileReader
    {
        IRunSettingsFile ReadFromFile(string path);
    }
}
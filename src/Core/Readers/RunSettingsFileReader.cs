using System.IO;
using VisualStudio.Files.Abstractions;
using VisualStudio.Files.Abstractions.RunSettings;
using VisualStudio.Files.Core.RunSettings;
using WrapThat.SystemIO;

namespace VisualStudio.Files.Core.Readers
{
    public class RunSettingsFileReader : FileReader<IRunSettingsFile>, IRunSettingsFileReader
    {
        private const string RunSettingsFileGlob = "*.runsettings";
        
        public RunSettingsFileReader(ISystemIO io) : base(io, RunSettingsFileGlob)
        {
        }

        protected override IRunSettingsFile ReadFromFileInfo(FileInfo fileInfo)
        {
            return new RunSettingsFile(fileInfo);
        }
    }
}
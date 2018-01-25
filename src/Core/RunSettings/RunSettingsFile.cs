using System;
using System.IO;
using System.Xml.Linq;
using VisualStudio.Files.Abstractions.RunSettings;

namespace VisualStudio.Files.Core.RunSettings
{
    public class RunSettingsFile : IRunSettingsFile
    {
        private readonly  XElement _xmlRoot;
        private readonly FileInfo _file;
        private readonly INUnitSection _nunitSection;

        internal RunSettingsFile(FileInfo fileInfo)
        {
            if (fileInfo == null)
            {
                throw new ArgumentNullException(nameof(fileInfo));
            }
                
            _file = fileInfo;
            _xmlRoot = XElement.Load(_file.FullName);
            _nunitSection = new NUnitSection(_xmlRoot);
        }

        public DirectoryInfo Directory => _file.Directory;
        public FileInfo File => _file;
        public XElement XmlRoot => _xmlRoot;
        public INUnitSection NUnit => _nunitSection;
    }
}
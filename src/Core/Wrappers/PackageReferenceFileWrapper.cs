using System.Collections.Generic;
using System.IO;
using System.Linq;
using NuGet;
using VisualStudio.Files.Abstractions;

namespace VisualStudio.Files.Core.Wrappers
{
    public class PackageReferenceFileWrapper : IPackageReferenceFile
    {
        private readonly PackageReferenceFile _packageReferenceFile;
        private readonly FileInfo _fileInfo;
        
        public PackageReferenceFileWrapper(string path)
        {
            _packageReferenceFile = new PackageReferenceFile(path);
            _fileInfo = new FileInfo(path);
        }

        public DirectoryInfo Directory => _fileInfo.Directory;
        public FileInfo File => _fileInfo;
        public IEnumerable<IPackageReference> PackageReferences => 
            _packageReferenceFile
                .GetPackageReferences(true)
                .Select(r => new PackageReferenceWrapper(r));
    }
}
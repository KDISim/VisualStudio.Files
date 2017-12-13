using System.IO;
using VisualStudio.Files.Abstractions;
using WrapThat.SystemIO;
using PackageReferenceFileWrapper = VisualStudio.Files.Core.Wrappers.PackageReferenceFileWrapper;

namespace VisualStudio.Files.Core.Readers
{
    public class PackageReferenceFileReader : FileReader<IPackageReferenceFile>, IPackageReferenceFileReader
    {
        private const string PackagesReferenceFileGlob = "packages.config";

        public PackageReferenceFileReader(ISystemIO io) : base(io, PackagesReferenceFileGlob)
        {
        }

        protected override IPackageReferenceFile ReadFromFileInfo(FileInfo fileInfo)
        {
            return new PackageReferenceFileWrapper(fileInfo.FullName);
        }
    }
}
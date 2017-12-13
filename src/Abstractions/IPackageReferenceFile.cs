using System.Collections.Generic;

namespace VisualStudio.Files.Abstractions
{
    public interface IPackageReferenceFile : IFileInfo
    {
        IEnumerable<IPackageReference> PackageReferences { get; }
    }
}
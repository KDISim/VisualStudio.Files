using System.Collections.Generic;

namespace VisualStudio.Files.Abstractions
{
    public interface IProject : IFileInfo
    {
        IEnumerable<IPackageReference> Packages { get; }
        bool HasPackageReferenceFile { get; }
    }
}
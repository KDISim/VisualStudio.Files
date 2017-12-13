using System;
using NuGet;
using VisualStudio.Files.Abstractions;

namespace VisualStudio.Files.Core.Wrappers
{
    public class PackageReferenceWrapper : IPackageReference
    {
        private readonly PackageReference _packageReference;
        internal PackageReferenceWrapper(PackageReference packageReference)
        {
            _packageReference = packageReference ?? throw new ArgumentNullException(nameof(packageReference));
        }

        public string Id => _packageReference.Id;
        public string Version => _packageReference.Version.ToNormalizedString();
    }
}
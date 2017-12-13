using System.Collections.Generic;

namespace VisualStudio.Files.Abstractions
{
    public interface ISolution : IFileInfo
    {
        string Name { get; }
        IEnumerable<IProject> Projects { get; }
    }
}
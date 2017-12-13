using System.IO;

namespace VisualStudio.Files.Abstractions
{
    public interface IFileInfo
    {
        DirectoryInfo Directory { get; }
        FileInfo File { get; }
    }
}
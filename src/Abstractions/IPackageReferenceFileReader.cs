namespace VisualStudio.Files.Abstractions
{
    public interface IPackageReferenceFileReader
    {
        IPackageReferenceFile ReadFromFile(string path);
    }
}
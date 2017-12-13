namespace VisualStudio.Files.Abstractions
{
    public interface IPackageReference
    {
        string Id { get; }
        string Version { get; }
    }
}
namespace VisualStudio.Files.Abstractions
{
    public interface IOutputPath
    {
        string Platform { get; }
        string Configuration { get; }
        string Path { get; }
    }
}
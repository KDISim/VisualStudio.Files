namespace VisualStudio.Files.Abstractions
{
    public interface ISolutionReader
    {
        ISolution ReadFromFile(string path);
    }
}
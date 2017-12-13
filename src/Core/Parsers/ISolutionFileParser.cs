using VisualStudio.Files.Core.Wrappers;

namespace VisualStudio.Files.Core.Parsers
{
    public interface ISolutionFileParser
    {
        ISolutionFile Parse(string path);
    }
}
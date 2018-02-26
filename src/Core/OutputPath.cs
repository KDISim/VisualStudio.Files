using System.Runtime.InteropServices;
using VisualStudio.Files.Abstractions;

namespace VisualStudio.Files.Core
{
    public class OutputPath : IOutputPath
    {
        private readonly string _platform;
        private readonly string _configuration;
        private readonly string _path;
        
        internal OutputPath(string platform, string configuration, string path)
        {
            _platform = platform;
            _configuration = configuration;
            _path = path;
        }

        public string Platform => _platform;
        public string Configuration => _configuration;
        public string Path => _path;
    }
}
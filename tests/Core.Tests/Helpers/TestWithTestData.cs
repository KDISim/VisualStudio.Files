using System.IO;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace VisualStudio.Files.Core.Tests.Helpers
{
    public abstract class TestWithTestData
    {
        private string _basePath;

        protected void SetBasePath(string basePath)
        {
            _basePath = basePath;
        }
        
        protected FileInfo File(string relativePath)
        {
            var runSettingsPath = Path.Combine(TestContext.CurrentContext.TestDirectory, _basePath);
            var path = Path.Combine(runSettingsPath, relativePath);
            return new FileInfo(path);
        }
    }
}
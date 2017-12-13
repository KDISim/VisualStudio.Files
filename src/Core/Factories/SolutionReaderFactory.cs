using System;
using Microsoft.Extensions.DependencyInjection;
using VisualStudio.Files.Abstractions;
using VisualStudio.Files.Core.Parsers;
using VisualStudio.Files.Core.Readers;
using WrapThat.SystemIO;

namespace VisualStudio.Files.Core.Factories
{
    public static class SolutionReaderFactory
    {
        public static ISolutionReader Create()
        {
            var collection = new ServiceCollection();
            collection.AddVisualStudioFiles();
            
            var provider = collection.BuildServiceProvider();

            return provider.GetService<ISolutionReader>();
        }
    }
}
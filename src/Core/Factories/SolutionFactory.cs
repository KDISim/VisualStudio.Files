using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using VisualStudio.Files.Abstractions;
using ISolutionFile = VisualStudio.Files.Core.Wrappers.ISolutionFile;

namespace VisualStudio.Files.Core.Factories
{
    internal class SolutionFactory : ISolutionFactory
    {
        private readonly IServiceProvider _serviceProvider;
        
        public SolutionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        
        public ISolution Create(ISolutionFile solutionFile, FileInfo fileInfo)
        {
            if (solutionFile == null) {throw new ArgumentNullException(nameof(solutionFile));}
            if (fileInfo == null) throw new ArgumentNullException(nameof(fileInfo));
            
            var projectFactory = _serviceProvider.GetService<IProjectFactory>();
            return new Solution(solutionFile, fileInfo, projectFactory);
        }
    }
}
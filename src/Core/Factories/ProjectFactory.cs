using System;
using Microsoft.Extensions.DependencyInjection;
using VisualStudio.Files.Abstractions;
using VisualStudio.Files.Core.Readers;
using WrapThat.SystemIO;
using IProjectInSolution = VisualStudio.Files.Core.Wrappers.IProjectInSolution;

namespace VisualStudio.Files.Core.Factories
{
    internal class ProjectFactory : IProjectFactory
    {
        private readonly IServiceProvider _serviceProvider;
        
        public ProjectFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        
        public IProject Create(IProjectInSolution projectInSolution)
        {
            var io = _serviceProvider.GetService<ISystemIO>();
            var runsettingsFileReader = _serviceProvider.GetService<IRunSettingsFileReader>();
            
            return new Project(projectInSolution, io, runsettingsFileReader);
        }
    }
}
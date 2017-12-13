using Microsoft.Build.Construction;

namespace VisualStudio.Files.Core.Wrappers
{
    internal class ProjectConfigurationInSolutionWrapper : IProjectConfigurationInSolution
    {
        private readonly ProjectConfigurationInSolution _configuration;
        
        internal ProjectConfigurationInSolutionWrapper(ProjectConfigurationInSolution configuration)
        {
            _configuration = configuration;
        }

        public string ConfigurationName => _configuration.ConfigurationName;
        public string PlatformName => _configuration.PlatformName;
        public string FullName => _configuration.FullName;
        public bool IncludeInBuild => _configuration.IncludeInBuild;
    }
}
using Microsoft.Build.Construction;

namespace VisualStudio.Files.Core.Wrappers
{
    internal class SolutionConfigurationInSolutionWrapper : Core.Wrappers.ISolutionConfigurationInSolution
    {
        private readonly SolutionConfigurationInSolution _configuration;
        
        internal SolutionConfigurationInSolutionWrapper(SolutionConfigurationInSolution configuration)
        {
            _configuration = configuration;
        }

        public string ConfigurationName => _configuration.ConfigurationName;
        public string PlatformName => _configuration.PlatformName;
        public string FullName => _configuration.FullName;
    }
}
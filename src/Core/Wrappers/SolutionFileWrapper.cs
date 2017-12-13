using System.Collections.Generic;
using System.Linq;
using Microsoft.Build.Construction;

namespace VisualStudio.Files.Core.Wrappers
{
    public class SolutionFileWrapper : Core.Wrappers.ISolutionFile
    {
        private readonly SolutionFile _solutionFile;
        public SolutionFileWrapper(string path)
        {
            _solutionFile = SolutionFile.Parse(path);
        }

        public IReadOnlyList<Core.Wrappers.IProjectInSolution> ProjectsInOrder => Wrap(_solutionFile.ProjectsInOrder);
        public IReadOnlyDictionary<string, Core.Wrappers.IProjectInSolution> ProjectsByGuid => Wrap(_solutionFile.ProjectsByGuid);

        public IReadOnlyList<Core.Wrappers.ISolutionConfigurationInSolution> SolutionConfigurations =>
            Wrap(_solutionFile.SolutionConfigurations);

        public string GetDefaultConfigurationName()
        {
            return _solutionFile.GetDefaultConfigurationName();
        }

        public string GetDefaultPlatformName()
        {
            return _solutionFile.GetDefaultPlatformName();
        }

        private IReadOnlyList<Core.Wrappers.IProjectInSolution> Wrap(IReadOnlyList<ProjectInSolution> source)
        {
            return source.Select(x => new ProjectInSolutionWrapper(x)).ToList();
        }
        
        private IReadOnlyDictionary<string, Core.Wrappers.IProjectInSolution> Wrap(IReadOnlyDictionary<string, ProjectInSolution> source)
        {
            var dictionary = new Dictionary<string, Core.Wrappers.IProjectInSolution>();

            foreach (var key in source.Keys)
            {
                dictionary.Add(key, new ProjectInSolutionWrapper(source[key]));
            }

            return dictionary;
        }
        
        private IReadOnlyList<Core.Wrappers.ISolutionConfigurationInSolution> Wrap(IReadOnlyList<SolutionConfigurationInSolution> source)
        {
            return source.Select(x => new SolutionConfigurationInSolutionWrapper(x)).ToList();
        }
    }
}
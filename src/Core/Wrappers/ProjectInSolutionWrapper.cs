using System.Collections.Generic;
using Microsoft.Build.Construction;

namespace VisualStudio.Files.Core.Wrappers
{
    internal class ProjectInSolutionWrapper : IProjectInSolution
    {
        private readonly ProjectInSolution _project;

        internal ProjectInSolutionWrapper(ProjectInSolution project)
        {
            _project = project;
        }

        public string ProjectName => _project.ProjectName;
        public string RelativePath => _project.RelativePath;
        public string AbsolutePath => _project.AbsolutePath;
        public string ProjectGuid => _project.ProjectGuid;
        public string ParentProjectGuid => _project.ParentProjectGuid;
        public IReadOnlyList<string> Dependencies => _project.Dependencies;

        public IReadOnlyDictionary<string, IProjectConfigurationInSolution> ProjectConfigurations =>
            Wrap(_project.ProjectConfigurations);

        private IReadOnlyDictionary<string, IProjectConfigurationInSolution> Wrap(
            IReadOnlyDictionary<string, ProjectConfigurationInSolution> source)
        {
            var dictionary = new Dictionary<string, IProjectConfigurationInSolution>();
            foreach (var key in source.Keys)
            {
                dictionary.Add(key, new ProjectConfigurationInSolutionWrapper(source[key]));
            }

            return dictionary;
        }
    }
}
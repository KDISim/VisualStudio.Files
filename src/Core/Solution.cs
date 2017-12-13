using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using VisualStudio.Files.Abstractions;
using VisualStudio.Files.Core.Factories;
using IProjectInSolution = VisualStudio.Files.Core.Wrappers.IProjectInSolution;
using ISolutionFile = VisualStudio.Files.Core.Wrappers.ISolutionFile;

namespace VisualStudio.Files.Core
{
    internal class Solution : ISolution
    {
        private const string CsProjExtension = ".csproj";
        private readonly ISolutionFile _solutionFile;
        private readonly FileInfo _fileInfo;
        private readonly IProjectFactory _projectFactory;
        
        internal Solution(ISolutionFile solutionFile, FileInfo solutionFileInfo, IProjectFactory projectFactory)
        {
            _solutionFile = solutionFile ?? throw new ArgumentNullException(nameof(solutionFile));
            _fileInfo = solutionFileInfo ?? throw new ArgumentNullException(nameof(solutionFileInfo));
            _projectFactory = projectFactory ?? throw new ArgumentNullException(nameof(projectFactory));
        }

        public DirectoryInfo Directory => _fileInfo.Directory;
        public FileInfo File => _fileInfo;
        public string Name => Path.GetFileNameWithoutExtension(_fileInfo.Name);

        public IEnumerable<IProject> Projects => Wrap(_solutionFile.ProjectsInOrder);

        private IEnumerable<IProject> Wrap(IEnumerable<IProjectInSolution> source)
        {
            return source
                .Where(p => p.AbsolutePath.EndsWith(CsProjExtension))
                .Select(p=> _projectFactory.Create(p));
        }
    }
}
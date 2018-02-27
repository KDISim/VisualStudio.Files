using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using VisualStudio.Files.Abstractions;
using VisualStudio.Files.Abstractions.RunSettings;
using VisualStudio.Files.Core.Readers;
using WrapThat.SystemIO;
using IProjectInSolution = VisualStudio.Files.Core.Wrappers.IProjectInSolution;

namespace VisualStudio.Files.Core
{
    internal class Project : IProject
    {
        private readonly FileInfo _projectFileInfo;
        private readonly ISystemIO _io;
        private readonly IRunSettingsFileReader _runSettingsFileReader;
        private readonly Microsoft.Build.Evaluation.Project _evaluatedProject;
        
        internal Project(IProjectInSolution projectInSolution, ISystemIO io, IRunSettingsFileReader runSettingsFileReader)
        {
            if (projectInSolution == null)
            {
                throw new ArgumentNullException(nameof(projectInSolution));
            }
            
            _io = io ?? throw new ArgumentNullException(nameof(io));
            _runSettingsFileReader =
                runSettingsFileReader ?? throw new ArgumentNullException(nameof(runSettingsFileReader));

            _projectFileInfo = new FileInfo(projectInSolution.AbsolutePath);
            
            var content = _io.File.ReadAllText(projectInSolution.AbsolutePath);
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(content ?? "")))
            using (var reader = XmlReader.Create(stream))
            {
                _evaluatedProject = new Microsoft.Build.Evaluation.Project(reader);

            }
        }
        
        public DirectoryInfo Directory => _projectFileInfo.Directory;
        public FileInfo File => _projectFileInfo;
        
        
        
        public bool TryGetOutputDirectory(string configuration, string platform, out DirectoryInfo outputDirectory)
        {
            _evaluatedProject.SetGlobalProperty("Configuration", configuration);
            _evaluatedProject.SetGlobalProperty("Platform", platform);
            _evaluatedProject.ReevaluateIfNecessary();
            
            var outputPathProperty = _evaluatedProject.GetProperty("OutputPath");

            if (outputPathProperty != null)
            {
                var absolutePath = Path.Combine(Directory.FullName, outputPathProperty.EvaluatedValue);
                outputDirectory =  new DirectoryInfo(absolutePath);
                return true;
            }
            
            outputDirectory = null;
            return false;
        }

        public IEnumerable<IRunSettingsFile> RunSettings => LoadRunSettings();

        private IEnumerable<IRunSettingsFile> LoadRunSettings()
        {
            return Directory
                .EnumerateFiles("*.runsettings", SearchOption.AllDirectories)
                .Select(file => _runSettingsFileReader.ReadFromFile(file.FullName));
        }
    }
}
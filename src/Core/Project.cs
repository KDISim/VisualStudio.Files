using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
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
        private readonly XElement _xmlRoot;
        
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
            
            var content = _io.File.ReadAllText(_projectFileInfo.FullName);
            _xmlRoot = XElement.Parse(content);
        }
        
        public DirectoryInfo Directory => _projectFileInfo.Directory;
        public FileInfo File => _projectFileInfo;
        
        public XElement XmlRoot => _xmlRoot;
        
        
        public IOutputPath GetOutputPath(string configuration, string platform)
        {
            var properties = LoadProperties(configuration, platform);

            var outputPathProperty = properties.SingleOrDefault(e => e.Name.Equals("OutputPath"));

            if (outputPathProperty == null)
            {
                throw new InvalidOperationException($"outputpath not found for configuration: {configuration} and platform: {platform}");    
            }

            var outputPath = ReplaceVars(outputPathProperty.Value, properties);
            
            return new OutputPath(platform, configuration, outputPath);
        }

        public IEnumerable<IRunSettingsFile> RunSettings => LoadRunSettings();
        
        private IEnumerable<IRunSettingsFile> LoadRunSettings()
        {
            return Directory
                .EnumerateFiles("*.runsettings", SearchOption.AllDirectories)
                .Select(file => _runSettingsFileReader.ReadFromFile(file.FullName));
        }

        private string ReplaceVars(string value, IEnumerable<IProperty> properties)
        {
            var replaced = value;
            foreach (var property in properties)
            {
                replaced = replaced.Replace($"$({property.Name})", property.Value);
            }

            return replaced;
        }

        private IEnumerable<IProperty> LoadProperties(string configuration, string platform)
        {
            var propertyGroups = _xmlRoot.Descendants()
                .Where(x => x.Name.LocalName.Equals("PropertyGroup"));

            var propertyGroupsInScope = propertyGroups
                .Where(p =>
                {
                    var attributes = p.Attributes();
                    var condition = attributes.FirstOrDefault(a => a.Name.LocalName.Equals("Condition"));

                    if (condition == null)
                    {
                        return true;
                    }

                    return
                        condition.Value.ToLower().Contains(configuration.ToLower()) &&
                        condition.Value.ToLower().Contains(platform.ToLower());
                });

            foreach (var group in propertyGroupsInScope)
            {
                foreach (var property in group.Descendants())
                {
                    yield return new Property(property.Name.LocalName, property.Value);
                }
            }
        }
    }
}
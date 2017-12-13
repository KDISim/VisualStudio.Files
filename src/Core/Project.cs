﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using VisualStudio.Files.Abstractions;
using WrapThat.SystemIO;
using IProjectInSolution = VisualStudio.Files.Core.Wrappers.IProjectInSolution;

namespace VisualStudio.Files.Core
{
    internal class Project : IProject
    {
        private static readonly IEnumerable<IPackageReference> NoReferences = new List<IPackageReference>();
        private readonly FileInfo _projectFileInfo;
        private readonly ISystemIO _io;
        private readonly IPackageReferenceFileReader _packageReferenceFileReader;
        
        internal Project(IProjectInSolution projectInSolution, ISystemIO io, IPackageReferenceFileReader packageReferenceFileReader)
        {
            if (projectInSolution == null)
            {
                throw new ArgumentNullException(nameof(projectInSolution));
            }

            _io = io ?? throw new ArgumentNullException(nameof(io));
            _packageReferenceFileReader = packageReferenceFileReader ?? throw new ArgumentNullException(nameof(packageReferenceFileReader));

            _projectFileInfo = new FileInfo(projectInSolution.AbsolutePath);
        }
        
        public DirectoryInfo Directory => _projectFileInfo.Directory;
        public FileInfo File => _projectFileInfo;
        public IEnumerable<IPackageReference> Packages => LoadPackageReferences();
        public bool HasPackageReferenceFile => PackagesConfigFileExists();

        private IEnumerable<IPackageReference> LoadPackageReferences()
        {
            if (HasPackageReferenceFile)
            {
                var path = GetPackagesConfigPath();
                var file = _packageReferenceFileReader.ReadFromFile(path);

                return file.PackageReferences;
            }
            else
            {
                return NoReferences;
            }
        }

        private bool PackagesConfigFileExists()
        {
            var path = GetPackagesConfigPath();

            return _io.File.Exists(path);
        }

        private string GetPackagesConfigPath()
        {
            return Path.Combine(Directory.FullName, "packages.config");
        }
       
    }
}
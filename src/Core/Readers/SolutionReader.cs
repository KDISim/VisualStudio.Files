using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using VisualStudio.Files.Abstractions;
using VisualStudio.Files.Core.Factories;
using VisualStudio.Files.Core.Parsers;
using WrapThat.SystemIO;

namespace VisualStudio.Files.Core.Readers
{

    internal class SolutionReader : FileReader<ISolution>, ISolutionReader
    {
        private const string SlnFileGlob = "*.sln";
        private readonly ISolutionFactory _solutionFactory;
        private readonly ISolutionFileParser _solutionFileParser;
        

        public SolutionReader(ISystemIO io, ISolutionFactory solutionFactory, ISolutionFileParser solutionFileParser) : base(io, SlnFileGlob)
        {
            _solutionFactory = solutionFactory ?? throw new ArgumentNullException(nameof(solutionFactory));
            _solutionFileParser = solutionFileParser ?? throw new ArgumentNullException(nameof(solutionFileParser));
        }

        protected override ISolution ReadFromFileInfo(FileInfo fileInfo)
        {
            var solutionFile = _solutionFileParser.Parse(fileInfo.FullName);
            return _solutionFactory.Create(solutionFile, fileInfo);
        }
    }
}
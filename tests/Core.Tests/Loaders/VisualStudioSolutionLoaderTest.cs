using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using VisualStudio.Files.Abstractions;
using VisualStudio.Files.Core.Factories;
using VisualStudio.Files.Core.Parsers;
using VisualStudio.Files.Core.Readers;
using WrapThat.SystemIO;

namespace VisualStudio.Files.Core.Tests.Loaders
{
    [TestFixture]
    public class VisualStudioSolutionTest
    {
        private ISystemIO _ioSubstitute;
        private ISolutionFileParser _solutionFileParserSubstitute;
        private ISolutionFactory _solutionFactorySubstitute;
        
        private SolutionReader _reader;
        

        [SetUp]
        public void Setup()
        {
            _ioSubstitute = Substitute.For<ISystemIO>();
            _solutionFileParserSubstitute = Substitute.For<ISolutionFileParser>();
            _solutionFactorySubstitute = Substitute.For<ISolutionFactory>();
            
            _reader = new SolutionReader(_ioSubstitute, _solutionFactorySubstitute, _solutionFileParserSubstitute);
        }

        private ISolution InvokeReadFromFile(string path)
        {
            return _reader.ReadFromFile(path);
        }
        
        [Test]
        public void ConstructorMustThrowsArgumentNullExceptionWhenIoIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(()=>new SolutionReader(null, _solutionFactorySubstitute, _solutionFileParserSubstitute));
            Assert.That(exception.Message, Does.Contain("io"));
        }
        
        [Test]
        public void ConstructorMustThrowsArgumentNullExceptionWhenSolutionFactoryIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(()=>new SolutionReader(_ioSubstitute, null, _solutionFileParserSubstitute));
            Assert.That(exception.Message, Does.Contain("solutionFactory"));
        }
        
        [Test]
        public void ConstructorMustThrowsArgumentNullExceptionWhenSolutionFileParserIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(()=>new SolutionReader(_ioSubstitute, _solutionFactorySubstitute, null));
            Assert.That(exception.Message, Does.Contain("solutionFileParser"));
        }
        [Test]
        public void ReadFromFileMustThrowArgumentExceptionWhenPathIsNull()
        {
            var exception = Assert.Throws<ArgumentException>(() => InvokeReadFromFile(null));
            Assert.That(exception.Message.StartsWith("Path cannot be null, empty or contain only whitespaces"));
        }
        
        [Test]
        public void ReadFromFileMustThrowArgumentExceptionWhenPathIsEmpty()
        {
            var exception = Assert.Throws<ArgumentException>(() => InvokeReadFromFile(string.Empty));
            Assert.That(exception.Message.StartsWith("Path cannot be null, empty or contain only whitespaces"));
        }
        
        [Test]
        public void ReadFromFileMustThrowArgumentExceptionWhenPathContainsOnlyWhitespaces()
        {
            var exception = Assert.Throws<ArgumentException>(() => InvokeReadFromFile("   "));
            Assert.That(exception.Message, Does.StartWith("Path cannot be null, empty or contain only whitespaces"));
        }

        [Test]
        public void ReadFromFileMustThrowFileNotFoundExceptionWhenPathIsNotAnExistingFileOrDirectory()
        {
            _ioSubstitute.File.Exists(Arg.Any<string>()).Returns(false);
            
            var exception = Assert.Throws<FileNotFoundException>(() => InvokeReadFromFile(@"c:\doesnt\matter"));
            Assert.That(exception.Message, Does.StartWith("Could not find file."));
        }

        [Test]
        public void ReadFromFileMustThrowArgumentExceptionWhenFileExistsButIsNotSlnFile()
        {
            _ioSubstitute.File.Exists(Arg.Any<string>()).Returns(true);

            var exception = Assert.Throws<ArgumentException>(() => InvokeReadFromFile(@"c:\path\to\some\file.txt"));
            Assert.That(exception.Message, Does.StartWith("File exists but file name does match expected glob pattern \"*.sln\""));
        }

        [Test]
        public void ReadFromFileMustParseSolutionFileWhenFileExistsAndIsAnSlnFile()
        {
            var expectedPath = @"c:\some\path\to\a\sln\file.sln";
            _ioSubstitute.File.Exists(Arg.Any<string>()).Returns(true);
            InvokeReadFromFile(@"c:\some\path\to\a\sln\file.sln");
            
            _solutionFileParserSubstitute.Received().Parse(expectedPath);
        }
        
    }
}
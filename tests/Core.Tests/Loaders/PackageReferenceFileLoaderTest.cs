using System;
using System.IO;
using NSubstitute;
using NUnit.Framework;
using VisualStudio.Files.Abstractions;
using VisualStudio.Files.Core.Readers;
using WrapThat.SystemIO;

namespace VisualStudio.Files.Core.Tests.Loaders
{
    [TestFixture]
    public class PackageReferenceFileLoaderTest
    {
        private ISystemIO _ioSubstitute;
        
        private PackageReferenceFileReader _fileReader;
       
        [SetUp]
        public void Setup()
        {
            _ioSubstitute = Substitute.For<ISystemIO>();
            
            _fileReader = new PackageReferenceFileReader(_ioSubstitute);
        }

        private IPackageReferenceFile InvokeReadFromFile(string path)
        {
            return _fileReader.ReadFromFile(path);
        }

        [Test]
        public void ConstructorMustThrowsArgumentNullExceptionWhenIoIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(()=>new PackageReferenceFileReader(null));
            Assert.That(exception.Message, Does.Contain("io"));
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
        public void ReadFromFileMustThrowFileNotFoundExceptionWhenPathIsNotAnExistingFile()
        {
            _ioSubstitute.File.Exists(Arg.Any<string>()).Returns(false);
            
            var exception = Assert.Throws<FileNotFoundException>(() => InvokeReadFromFile(@"c:\doesnt\matter"));
            Assert.That(exception.Message, Does.StartWith("Could not find file."));
        }

        [Test]
        public void ReadFromFileMustThrowArgumentExceptionWhenFileExistsButIsNotCsProjectFile()
        {
            _ioSubstitute.File.Exists(Arg.Any<string>()).Returns(true);

            var exception = Assert.Throws<ArgumentException>(() => InvokeReadFromFile(@"c:\path\to\some\file.txt"));
            Assert.That(exception.Message, Does.StartWith("File exists but file name does match expected glob pattern \"packages.config\""));
        }

        [Test]
        public void ReadFromFileMustLoadPackagesConfigFileWhenFileExists()
        {
            _ioSubstitute.File.Exists(Arg.Any<string>()).Returns(true);

            _fileReader.ReadFromFile(@"c:\path\to\some\project\packages.config");
        }
    }
}
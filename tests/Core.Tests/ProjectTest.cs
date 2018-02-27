using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using NSubstitute;
using NUnit.Framework;
using VisualStudio.Files.Core.Readers;
using VisualStudio.Files.Core.Tests.TestData;
using VisualStudio.Files.Core.Wrappers;
using WrapThat.SystemIO;

namespace VisualStudio.Files.Core.Tests
{
    [TestFixture]
    public class ProjectTest
    {
        private const string DefaultProjectDirectory = @"c:\repos\root\some\project\";
        private const string DefaultProjectFile = "file.csproj";

        private static readonly string DefaultProjectFilePath =
            Path.Combine(DefaultProjectDirectory, DefaultProjectFile);
        
        private IProjectInSolution _projectInSolutionSubstitute;
        private ISystemIO _ioSubstitute;
        private IRunSettingsFileReader _runSettingsFileReaderSubstitute;

        private Project Create()
        {
            return new Project(_projectInSolutionSubstitute, _ioSubstitute, _runSettingsFileReaderSubstitute);
        }
        
        private void StubProjectFileContent(string content)
        {
            _ioSubstitute.File.ReadAllText(Arg.Any<string>()).Returns(content);
        }

        [SetUp]
        public void SetUp()
        {
            _projectInSolutionSubstitute = Substitute.For<IProjectInSolution>();
            _ioSubstitute = Substitute.For<ISystemIO>();
            _runSettingsFileReaderSubstitute = Substitute.For<IRunSettingsFileReader>();
            
            _projectInSolutionSubstitute.AbsolutePath.Returns(DefaultProjectFilePath);
        }
        
        [Test]
        public void ConstructorMustThrowArgumentNullExceptionWhenProjectInSolutionIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new Project(null, _ioSubstitute, _runSettingsFileReaderSubstitute));
            Assert.That(exception.Message, Contains.Substring("projectInSolution"));
        }
        
        [Test]
        public void ConstructorMustThrowArgumentNullExceptionWhenIoIsNull()
        {
            StubProjectFileContent(ProjectFiles.Empty);
            
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new Project(_projectInSolutionSubstitute, null, _runSettingsFileReaderSubstitute));
            Assert.That(exception.Message, Contains.Substring("io"));
        }
        
        [Test]
        public void ConstructorMustThrowArgumentNullExceptionWhenRunSettingsFileReaderIsNull()
        {
            StubProjectFileContent(ProjectFiles.Empty);
            
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new Project(_projectInSolutionSubstitute, _ioSubstitute, null));
            Assert.That(exception.Message, Contains.Substring("runSettingsFileReader"));
        }

        /*
        [Test]
        public void TryGetPropertyMustReturnFalseWhenProjectFileIsEmpty()
        {
            StubProjectFileContent(ProjectFiles.Empty);
            var project = Create();
            
            var result = project.TryGetProperty()
        }
        */
        
        [Test]
        public void TryGetOutputMustReturnFalseWhenProjectFileIsEmpty()
        {
            StubProjectFileContent(ProjectFiles.Empty);
            var project = Create();

            var result = project.TryGetOutputDirectory("something", "something", out DirectoryInfo actual);
            
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void TryGetOutputDirectoryMustReturnFalseWhenConfigutationAndPlatfromAreNotInProjectFile()
        {
            StubProjectFileContent(ProjectFiles.WithSingleOutputPathWithVars);
            var project = Create();

            var result = project.TryGetOutputDirectory("something", "something", out DirectoryInfo actual);
            
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void TryGetOutputDirectoryMustReturnOutputPathWhenProjectContainsOneOutputPathWithVars()
        {
            
            StubProjectFileContent(ProjectFiles.WithSingleOutputPathWithVars);
            var project = Create();
            var expectedPath = Path.Combine(DefaultProjectDirectory, @"..\..\bin\x86d\");
            var expectedDirectory = new DirectoryInfo(expectedPath);

            var result = project.TryGetOutputDirectory("debug", "x86", out DirectoryInfo actual);
            
            Assert.That(result, Is.True);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.FullName, Is.EqualTo(expectedDirectory.FullName));

        }
        
        [Test]
        public void TryGetOutputDirectoryMustReturnOutputPathWhenProjectContainsOneOutputPathWithoutVars()
        {
            StubProjectFileContent(ProjectFiles.WithSingleOutputPathWithoutVars);
            var project = Create();
            
            var expectedPath = Path.Combine(DefaultProjectDirectory, @"..\..\bin\x86d\");
            var expectedDirectory = new DirectoryInfo(expectedPath);

            var result = project.TryGetOutputDirectory("debug", "x86", out DirectoryInfo actual);
            
            Assert.That(result, Is.True);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.FullName, Is.EqualTo(expectedDirectory.FullName));

        }
        
        [Test]
        public void TryGetOutputDirectoryMustReturnOutputPathWhenProjectContainsMultipleOutPuthPathWithVars()
        {
            StubProjectFileContent(ProjectFiles.WithMultipleOutputPathsWithVars);
            var project = Create();
            
            var expectedPath = Path.Combine(DefaultProjectDirectory, @"..\..\bin\x64\");
            var expectedDirectory = new DirectoryInfo(expectedPath);

            var result = project.TryGetOutputDirectory("release", "x64", out DirectoryInfo actual);
            
            Assert.That(result, Is.True);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.FullName, Is.EqualTo(expectedDirectory.FullName));

        }
    }
}
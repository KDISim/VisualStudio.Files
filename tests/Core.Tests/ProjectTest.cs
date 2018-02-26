using System;
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
            
            _projectInSolutionSubstitute.AbsolutePath.Returns(@"c:\some\project\file.csproj");
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
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new Project(_projectInSolutionSubstitute, null, _runSettingsFileReaderSubstitute));
            Assert.That(exception.Message, Contains.Substring("io"));
        }
        
        [Test]
        public void ConstructorMustThrowArgumentNullExceptionWhenRunSettingsFileReaderIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new Project(_projectInSolutionSubstitute, _ioSubstitute, null));
            Assert.That(exception.Message, Contains.Substring("runSettingsFileReader"));
        }
        
        [Test]
        public void ConstructorMustSetXmlRoot()
        {
            StubProjectFileContent(ProjectFiles.Empty);
            var expectedXml = XElement.Parse(ProjectFiles.Empty);
            var project = Create();
            
            Assert.Multiple(() => {
                Assert.That(project.XmlRoot, Is.Not.Null);
                Assert.That(project.XmlRoot.ToString(), Is.EqualTo(expectedXml.ToString()));
            });

        }
        
        [Test]
        public void OutputPathsMustThrowInvallidOperationExceptionWhenProjectFileIsEmpty()
        {
            StubProjectFileContent(ProjectFiles.Empty);
            var project = Create();
            
            Assert.Throws<InvalidOperationException>(() => project.GetOutputPath("something", "something"));
        }
        
        [Test]
        public void OutputPathsMustThrowInvallidOperationExceptionWhenConfigutationAndPlatfromAreNotInProjectFile()
        {
            StubProjectFileContent(ProjectFiles.Empty);
            var project = Create();
            
            Assert.Throws<InvalidOperationException>(() => project.GetOutputPath("something", "something"));
        }
        
        [Test]
        public void OutputPathsMustReturnOutputPathWhenProjectContainsOneOutputPathWithVars()
        {
            StubProjectFileContent(ProjectFiles.WithSingleOutputPathWithVars);
            var project = Create();

            var expectedConfiguration = "debug";
            var expectedPlatform = "x86";
            var expectedPath = "..\\..\\..\\bin\\x86d\\";

            var actual = project.GetOutputPath(expectedConfiguration, expectedPlatform);
            
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Path, Is.EqualTo(expectedPath));
            Assert.That(actual.Configuration, Is.EqualTo(expectedConfiguration));
            Assert.That(actual.Platform, Is.EqualTo(expectedPlatform));

        }
        
        [Test]
        public void OutputPathsMustReturnOutputPathWhenProjectContainsOneOutputPathWithoutVars()
        {
            StubProjectFileContent(ProjectFiles.WithSingleOutputPathWithoutVars);
            var project = Create();

            var expectedConfiguration = "debug";
            var expectedPlatform = "x86";
            var expectedPath = "..\\..\\..\\bin\\x86d\\";

            var actual = project.GetOutputPath(expectedConfiguration, expectedPlatform);
            
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Path, Is.EqualTo(expectedPath));
            Assert.That(actual.Configuration, Is.EqualTo(expectedConfiguration));
            Assert.That(actual.Platform, Is.EqualTo(expectedPlatform));

        }
        
        [Test]
        public void OutputPathsMustReturnOutputPathWhenProjectContainsMultipleOutPuthPathWithVars()
        {
            StubProjectFileContent(ProjectFiles.WithMultipleOutputPathsWithVars);
            var project = Create();

            var expectedConfiguration = "release";
            var expectedPlatform = "x64";
            var expectedPath = "..\\..\\..\\bin\\x64\\";

            var actual = project.GetOutputPath(expectedConfiguration, expectedPlatform);
            
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Path, Is.EqualTo(expectedPath));
            Assert.That(actual.Configuration, Is.EqualTo(expectedConfiguration));
            Assert.That(actual.Platform, Is.EqualTo(expectedPlatform));

        }
    }
}
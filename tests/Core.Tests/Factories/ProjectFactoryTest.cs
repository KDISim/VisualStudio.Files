using System;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using VisualStudio.Files.Abstractions;
using VisualStudio.Files.Core.Factories;
using VisualStudio.Files.Core.Readers;
using VisualStudio.Files.Core.Wrappers;
using WrapThat.SystemIO;

namespace VisualStudio.Files.Core.Tests.Factories
{
    [TestFixture]
    public class ProjectFactoryTest
    {
        private ProjectFactory _projectFactory;
        private IServiceProvider _serviceProviderSubstitute;

        [SetUp]
        public void SetUp()
        {
            _serviceProviderSubstitute = Substitute.For<IServiceProvider>();
            
            _projectFactory = new ProjectFactory(_serviceProviderSubstitute);
        }

        [Test]
        public void ConstructorMustThrowArgumentNullExceptionWhenServiceProviderIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ProjectFactory(null));
            Assert.That(exception.Message, Does.Contain("serviceProvider"));
        }
        
        [Test]
        public void CreateMustThrowArgumentNullExceptionWhenProjectInSolutionIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _projectFactory.Create(null));
            Assert.That(exception.Message, Does.Contain("projectInSolution"));
        }
        
        [Test]
        public void CreateMustCreateInstanceOfISolution()
        {
            var ioSubstitute = Substitute.For<ISystemIO>();
            var packageReferenceFileReaderSubstitute = Substitute.For<IPackageReferenceFileReader>();
            var runSettingsFileReaderSubstitute = Substitute.For<IRunSettingsFileReader>();
            
            _serviceProviderSubstitute.GetService<ISystemIO>().Returns(ioSubstitute);
            _serviceProviderSubstitute.GetService<IPackageReferenceFileReader>().Returns(packageReferenceFileReaderSubstitute);
            _serviceProviderSubstitute.GetService<IRunSettingsFileReader>().Returns(runSettingsFileReaderSubstitute);

            var projectInSolutionSubstitute = Substitute.For<IProjectInSolution>();
            projectInSolutionSubstitute.AbsolutePath.Returns(@"c:\some\random\path.txt");
            
            var instance = _projectFactory.Create(projectInSolutionSubstitute);
            Assert.That(instance, Is.Not.Null);
        }
    }
}
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using VisualStudio.Files.Core.Factories;
using VisualStudio.Files.Core.Wrappers;

namespace VisualStudio.Files.Core.Tests.Factories
{
    [TestFixture]
    public class SolutionFactoryTest
    {
        private static readonly FileInfo DefaultFileInfo = new FileInfo(@"c:\some\path.txt");
        private static readonly  ISolutionFile DefaultSolutionFile = Substitute.For<ISolutionFile>();
        private SolutionFactory _solutionFactory;
        private IServiceProvider _serviceProviderSubstitute;

        [SetUp]
        public void SetUp()
        {
            _serviceProviderSubstitute = Substitute.For<IServiceProvider>();
            
            _solutionFactory = new SolutionFactory(_serviceProviderSubstitute);
        }

        [Test]
        public void ConstructorMustThrowArgumentNullExceptionWhenServiceProviderIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new SolutionFactory(null));
            Assert.That(exception.Message, Does.Contain("serviceProvider"));
        }
        
        [Test]
        public void CreateMustThrowArgumentNullExceptionWhenSolutionFileIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _solutionFactory.Create(null, DefaultFileInfo));
            Assert.That(exception.Message, Does.Contain("solutionFile"));
        }
        
        [Test]
        public void CreateMustThrowArgumentNullExceptionWhenFileInfoIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _solutionFactory.Create(DefaultSolutionFile, null));
            Assert.That(exception.Message, Does.Contain("fileInfo"));
        }

        [Test]
        public void CreateMustCreateInstanceOfISolution()
        {
            var projectFactorySubstitute = Substitute.For<IProjectFactory>();
            _serviceProviderSubstitute.GetService<IProjectFactory>().Returns(projectFactorySubstitute);
            
            var instance = _solutionFactory.Create(DefaultSolutionFile, DefaultFileInfo);
            Assert.That(instance, Is.Not.Null);
        }
    }
}
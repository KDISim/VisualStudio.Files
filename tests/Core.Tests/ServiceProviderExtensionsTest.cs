using System;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute.Exceptions;
using NUnit.Framework;
using VisualStudio.Files.Abstractions;
using VisualStudio.Files.Core.Factories;
using VisualStudio.Files.Core.Parsers;
using VisualStudio.Files.Core.Readers;
using WrapThat.SystemIO;

namespace VisualStudio.Files.Core.Tests
{
    [TestFixture]
    public class ServiceProviderExtensionsTest
    {
        private IServiceProvider _provider;

        [SetUp]
        public void SetUp()
        {
            var collection = new ServiceCollection();
            collection.AddVisualStudioFiles();
            _provider = collection.BuildServiceProvider();
        }

        [Test]
        public void MustBeAbleToConstructISystemIoInstance()
        {
            var instance = _provider.GetService<ISystemIO>();
            Assert.That(instance, Is.Not.Null);
        }
        
        [Test]
        public void MustBeAbleToConstructISolutionFileParserInstance()
        {
            var instance = _provider.GetService<ISolutionFileParser>();
            Assert.That(instance, Is.Not.Null);
        }
        
        [Test]
        public void MustBeAbleToConstructRunSettingsFileReaderInstance()
        {
            var instance = _provider.GetService<IRunSettingsFileReader>();
            Assert.That(instance, Is.Not.Null);
        }
        
        [Test]
        public void MustBeAbleToConstructIProjectFactoryInstance()
        {
            var instance = _provider.GetService<IProjectFactory>();
            Assert.That(instance, Is.Not.Null);
        }
        
        [Test]
        public void MustBeAbleToConstructISolutionFactoryInstance()
        {
            var instance = _provider.GetService<ISolutionFactory>();
            Assert.That(instance, Is.Not.Null);
        }
        
        [Test]
        public void MustBeAbleToConstructISolutionReaderInstance()
        {
            var instance = _provider.GetService<ISolutionReader>();
            Assert.That(instance, Is.Not.Null);
        }
    }
}
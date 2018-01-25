using System;
using System.IO;
using NUnit.Framework;
using VisualStudio.Files.Core.RunSettings;
using VisualStudio.Files.Core.Tests.Helpers;

namespace VisualStudio.Files.Core.Tests.RunSettings
{
    [TestFixture]
    public class RunSettingsFileTest : TestWithTestData
    {
        [SetUp]
        public void SetUp()
        {
            SetBasePath(@"RunSettings\TestData");
        }
        
        [Test]
        public void ConstructorMustThrowsArgumentNullExceptionWhenPathIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(()=>new RunSettingsFile(null));
            Assert.That(exception.Message, Does.Contain("fileInfo"));
        }
        
        [Test]
        public void ConstructorMustLoadRunSettingsFileWhenFileInfoIsProvided()
        {
            var runsettings = new RunSettingsFile(File(@"Empty.runsettings"));
            Assert.That(runsettings, Is.Not.Null);
        }
        
        [Test]
        public void ConstructorMustLoadNUnitSectionWhenItExists()
        {
            var runsettings = new RunSettingsFile(File(@"NUnit.runsettings"));
            Assert.That(runsettings.NUnit.Exists, Is.True);
        }
    }
}
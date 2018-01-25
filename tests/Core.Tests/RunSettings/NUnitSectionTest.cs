using System.IO;
using System.Xml.Linq;
using NUnit.Framework;
using VisualStudio.Files.Core.RunSettings;
using VisualStudio.Files.Core.Tests.Helpers;

namespace VisualStudio.Files.Core.Tests.RunSettings
{
    [TestFixture]
    public class NUnitSectionTest: TestWithTestData
    {
        [SetUp]
        public void SetUp()
        {
            SetBasePath(@"RunSettings\TestData");
        }

        private NUnitSection Load(FileInfo file)
        {
            var root = XElement.Load(file.FullName);
            return new NUnitSection(root);
        }
        
        [Test]
        public void ExistsMustReturnFalseWhenNUnitSectionDoesNotExist()
        {
            var section = Load(File("Empty.runsettings"));
            Assert.That(section.Exists, Is.False);
        }
        
        [Test]
        public void ExistsMustReturnTrueWhenNUnitSectionExists()
        {
            var section = Load(File("NUnit.runsettings"));
            Assert.That(section.Exists, Is.True);
        }
        
        [Test]
        public void DefaultTimeOutMustReturn60000WhenSetTo60000InRunSettingsFile()
        {
            var section = Load(File("NUnit.runsettings"));
            Assert.That(section.DefaultTimeOut, Is.EqualTo(60000));
        }
    }
}
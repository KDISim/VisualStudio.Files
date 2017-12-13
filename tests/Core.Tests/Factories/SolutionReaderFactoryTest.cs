using NUnit.Framework;
using VisualStudio.Files.Core.Factories;

namespace VisualStudio.Files.Core.Tests.Factories
{
    [TestFixture]
    public class SolutionReaderFactoryTest
    {
        [Test]
        public void MustCreateSolutionReaderInstance()
        {
            var instance = SolutionReaderFactory.Create();
            Assert.That(instance, Is.Not.Null);
        }
    }
}
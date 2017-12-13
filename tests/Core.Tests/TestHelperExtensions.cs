using System.Collections.Generic;
using System.Linq;

namespace VisualStudio.Files.Core.Tests
{
    internal static class TestHelperExtensions
    {
        internal static IEnumerable<T> ForceIEnumerableExecution<T>(this IEnumerable<T> source)
        {
            return source.ToList();
        }
    }
}
using System.Collections.Generic;

namespace EasySharpStandard.Collections.Core
{
    public static class ItemExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(T item)
        {
            return new[] { item };
        }
    }
}

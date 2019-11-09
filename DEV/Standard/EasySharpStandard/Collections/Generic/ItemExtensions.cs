using System.Collections.Generic;

namespace EasySharp.Collections.Generic
{
    public static class ItemExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(T item)
        {
            return new[] { item };
        }
    }
}

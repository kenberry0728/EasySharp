using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EasySharp.Collections
{
    public static class ListExtensions
    {
        public static IEnumerable<object> ToEnumerable(this IList list)
        {
            return list.OfType<object>();
        }

        public static IEnumerable<T> ToEnumerable<T>(this IList list)
        {
            return list.OfType<T>();
        }
    }
}

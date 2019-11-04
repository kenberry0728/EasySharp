using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EasySharpStandard.Collections.Core
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

        public static bool Contains<T>(this IList<T> list, Func<T, bool> predicate)
        {
            return list.Any(i => predicate(i));
        }

        public static void Remove<T>(this List<T> list, Func<T, bool> predicate)
        {
            var index = list.FindIndex(t => predicate(t));
            if (0 < index)
            {
                list.RemoveAt(index);
            }
        }
    }
}

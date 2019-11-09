using System;
using System.Collections.Generic;
using System.Linq;

namespace EasySharp.Collections.Generic
{
    public static class ListExtensions
    {
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

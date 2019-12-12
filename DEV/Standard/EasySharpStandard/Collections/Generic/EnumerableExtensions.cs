using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EasySharp.Collections.Generic
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Extensions")]
    public static class EnumerableExtensions
    {
        public static bool SequenceEqual<T>(
            this IEnumerable<T> collection1,
            IEnumerable<T> collection2,
            Func<T, T, bool> equal)
        {
            var collection1List = collection1 as IList<T> ?? collection1.ToList();
            var collection2List = collection2 as IList<T> ?? collection2.ToList();

            if (collection1List.Count != collection2List.Count)
            {
                return false;
            }

            for (int i = 0; i < collection1List.Count; i++)
            {
                if (!equal(collection1List[i], collection2List[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static List<T> ToList<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            return items.Where(predicate).ToList();
        }
    }
}

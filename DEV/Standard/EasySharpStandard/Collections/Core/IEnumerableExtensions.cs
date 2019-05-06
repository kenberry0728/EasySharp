using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasySharpStandard.Collections.Core
{
    public static class IEnumerableExtensions
    {
        public static bool SequenceEqual<T>(
            this IEnumerable<T> collection1,
            IEnumerable<T> collection2, 
            Func<T, T, bool> equal)
        {           var collection1List = collection1 as IList<T> ?? collection1.ToList();
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
    }
}

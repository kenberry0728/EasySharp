using System.Collections;
using System.Collections.Generic;

namespace EasySharp.Collections.Generic
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Extensions")]
    public static class CollectionExtensions
    {
        public static object SyncRoot<T>(this ICollection<T> items)
        {
            return ((ICollection)items).SyncRoot;
        }
    }
}

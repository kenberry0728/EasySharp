using System;
using System.Collections.Generic;
using System.Linq;

namespace EasySharp.Collections.Generic
{
    public static class ArrayExtensions
    {
        public static IEnumerable<object> ToEnumerable(this Array array)
        {
            return array.OfType<object>();
        }

        public static IEnumerable<T> ToEnumerable<T>(this Array array)
        {
            return array.OfType<T>();
        }

        public static IEnumerable<T> ToEnumerable<T>(this T[] array)
        {
            return array.OfType<T>();
        }
    }
}

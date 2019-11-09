using System.Collections.Generic;
using System.Linq;

namespace EasySharp.Collections.Generic
{
    public static class ConcatExtensions
    {
        public static IEnumerable<T> Concat<T>(this T value, IEnumerable<T> values)
        {
            return new[] { value }.Concat(values);
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> values, T value)
        {
            return values.Concat(new[] { value });
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace EasySharpStandard.Collections.Core
{
    public static class ConcatExtensions
    {
        public static IEnumerable<T> Concat<T>(this T value, IEnumerable<T> values)
        {
            return new T[] { value }.Concat(values);
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> values, T value)
        {
            return values.Concat(new T[] { value });
        }
    }
}

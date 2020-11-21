using System.Collections.Generic;
using System.Linq;

namespace EasySharp
{
    public static class StringsExtensions
    {
        public static string Join(this IEnumerable<string> strings, string separator)
        {
            return string.Join(separator, strings.ToArray());
        }

    }
}

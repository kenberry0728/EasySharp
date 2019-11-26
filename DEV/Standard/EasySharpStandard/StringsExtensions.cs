using System.Collections.Generic;
using System.Linq;

namespace EasySharp
{
    public static class StringsExtensions
    {
        public static string JoinWithTab(this IEnumerable<string> strings)
        {
            return string.Join("\t", strings.ToArray());
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EasySharpStandard.RegularExpressions.Core
{
    public static class RegexExtensions
    {
        public static bool AnyIsMatch(this IEnumerable<Regex> regularExpressions, string input)
        {
            return regularExpressions.Any(r => r.IsMatch(input));
        }
    }
}
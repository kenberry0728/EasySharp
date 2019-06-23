using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EasySharpStandard.DiskIO;

namespace AppInstaller
{
    public static class FileInfoExtensions
    {
        public static bool IsTargetFile(this string fileFullName, string baseDirectory, IEnumerable<Regex> excludeRegex)
        {
            // Extension is not good.
            return !excludeRegex.All(ex => ex.IsMatch(fileFullName.GetRelativePath(baseDirectory)));
        }

    }
}
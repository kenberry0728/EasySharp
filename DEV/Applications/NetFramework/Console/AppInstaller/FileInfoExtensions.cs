using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using EasySharpStandard.DiskIO;

namespace AppInstaller
{
    public static class FileInfoExtensions
    {
        public static bool IsTargetFile(this FileSystemInfo f, string baseDirectory, IEnumerable<Regex> excludeRegex)
        {
            return !excludeRegex.All(ex => ex.IsMatch(f.FullName.GetRelativePath(baseDirectory)));
        }

    }
}
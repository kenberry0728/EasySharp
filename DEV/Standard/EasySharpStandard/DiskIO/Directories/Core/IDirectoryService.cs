using System.Collections.Generic;
using System.IO;

namespace EasySharpStandard.DiskIO.Directories.Core
{
    public interface IDirectoryService
    {
        IEnumerable<string> GetFiles(string directoryPath, string searchPattern, SearchOption searchOption);
        string GetFullName(string sourceDir);
    }
}
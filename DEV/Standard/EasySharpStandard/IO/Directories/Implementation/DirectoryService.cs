using System.Collections.Generic;
using System.IO;
using System.Linq;
using EasySharp.IO.Directories.Core;

namespace EasySharp.IO.Directories.Implementation
{
    public class DirectoryService : IDirectoryService
    {
        public IEnumerable<string> GetFiles(string directoryPath, string searchPattern, SearchOption searchOption)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);
            return directoryInfo.GetFiles(searchPattern, searchOption).Select(f => f.FullName);
        }
    }
}

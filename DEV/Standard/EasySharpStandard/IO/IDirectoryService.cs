using System.Collections.Generic;
using System.IO;

namespace EasySharp.IO
{
    public interface IDirectoryService
    {
        IEnumerable<string> GetFiles(string directoryPath, string searchPattern, SearchOption searchOption);
    }
}
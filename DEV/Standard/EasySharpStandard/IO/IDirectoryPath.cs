using System;
using System.Collections.Generic;
using System.IO;

namespace EasySharp.IO
{
    public interface IDirectoryPath : IPathObjectBase
    {
        void CopyDirectory(
            IDirectoryPath directoryPath,
            bool overwrite = true,
            bool copySubDirs = true, 
            ICollection<string> excludeFileRelativePaths = null);
        void CreateDirectoryRecursively();
        void DeleteDirectoryRecursively();
        IEnumerable<string> GetFiles(string searchPattern, SearchOption searchOption);
        void ScopedSetCurrentDirectory(Action action);
        void SetLastWriteTimeToAllFiles(DateTime lastWriteTime);
        DirectoryPath ToFullDirectoryPath();
    }
}
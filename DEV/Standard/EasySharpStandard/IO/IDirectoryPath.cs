using System;
using System.Collections.Generic;
using System.IO;

namespace EasySharp.IO
{
    public interface IDirectoryPath
    {
        bool IsAbolutePath { get; }

        void CopyDirectory(DirectoryPath directoryPath, bool overwrite = true, bool copySubDirs = true);
        void CreateDirectoryRecursively();
        void DeleteDirectoryRecursively();
        bool Equals(object obj);
        IEnumerable<string> GetFiles(string searchPattern, SearchOption searchOption);
        int GetHashCode();
        void ScopedSetCurrentDirectory(Action action);
        void SetLastWriteTimeToAllFiles(DateTime lastWriteTime);
        DirectoryPath ToFullDirectoryPath();
        string ToString();
    }
}
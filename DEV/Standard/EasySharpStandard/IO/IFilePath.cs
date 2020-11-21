using System;
using System.Collections.Generic;

namespace EasySharp.IO
{
    public interface IFilePath : IPathObjectBase
    {
        void Copy(IFilePath targetFilePath, bool overwrite = true, bool ensureDirectoryForFile = true);
        void EnsureDirectory();
        DateTime GetLastWriteTimeUtc();
        IFilePath GetRelativePath(IDirectoryPath relativeDirectoryPath);
        IFilePath ToFullPath();
        string ReadAllText();
        IEnumerable<string> ReadLines(bool removeEmptyLine = false);
    }
}
using System;

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
    }
}
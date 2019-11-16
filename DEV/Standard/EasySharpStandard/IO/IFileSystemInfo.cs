using System;
using System.IO;

namespace EasySharp.IO
{
    public interface IFileSystemInfo
    {
        string FullPath { get; set; }
        string OriginalPath { get; set; }
        string FullName { get; }
        string Extension { get; }
        string Name { get; }
        bool Exists { get; }
        void Delete();
        DateTime CreationTime { get; set; }
        DateTime CreationTimeUtc { get; set; }
        DateTime LastAccessTime { get; set; }
        DateTime LastAccessTimeUtc { get; set; }
        DateTime LastWriteTime { get; set; }
        DateTime LastWriteTimeUtc { get; set; }
        FileAttributes Attributes { get; set; }
        void Refresh();
    }
}
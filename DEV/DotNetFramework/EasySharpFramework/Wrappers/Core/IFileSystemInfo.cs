using System;
using System.IO;
using System.Runtime.Serialization;

namespace EasySharpStandard.Wrappers.Core
{
    public interface IFileSystemInfo
    {
        string FullPath { get; set; }
        string OriginalPath { get; set; }
        void GetObjectData(SerializationInfo info, StreamingContext context);
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
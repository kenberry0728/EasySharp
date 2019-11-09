using System;

namespace EasySharp.IO.Files.Core
{
    public interface IFileService
    {
        DateTime GetLastWriteTimeUtc(string filePath);
    }
}
using System;

namespace EasySharp.IO
{
    public interface IFileService
    {
        DateTime GetLastWriteTimeUtc(string filePath);
    }
}
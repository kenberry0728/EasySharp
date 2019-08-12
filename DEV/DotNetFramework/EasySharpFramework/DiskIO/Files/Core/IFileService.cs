using System;

namespace EasySharpStandard.DiskIO.Files.Core
{
    public interface IFileService
    {
        DateTime GetLastWriteTimeUtc(string filePath);
    }
}
using System;
using System.IO;
using EasySharpStandard.DiskIO.Files.Core;

namespace EasySharpStandard.DiskIO.Files.Implementation
{
    public class FileService : IFileService
    {
        public DateTime GetLastWriteTimeUtc(string filePath)
        {
            return new FileInfo(filePath).LastWriteTimeUtc;
        }
    }
}
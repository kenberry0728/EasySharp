using System;
using System.IO;
using EasySharp.IO.Files.Core;

namespace EasySharp.IO.Files.Implementation
{
    public class FileService : IFileService
    {
        public DateTime GetLastWriteTimeUtc(string filePath)
        {
            return new FileInfo(filePath).LastWriteTimeUtc;
        }
    }
}
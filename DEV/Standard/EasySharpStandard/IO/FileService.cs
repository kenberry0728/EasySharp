using System;
using System.IO;

namespace EasySharp.IO
{
    public class FileService : IFileService
    {
        public DateTime GetLastWriteTimeUtc(string filePath)
        {
            return new FileInfo(filePath).LastWriteTimeUtc;
        }
    }
}
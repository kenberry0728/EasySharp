using System.IO;

namespace EasySharpStandard.DiskIO.Extensions
{
    public static class FilePathExtensions
    {
        public static string ReadToEnd(this string filePath)
        {
            using (var streamReader = new StreamReader(filePath))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}

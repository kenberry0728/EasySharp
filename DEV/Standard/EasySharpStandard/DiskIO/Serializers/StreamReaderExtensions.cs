using System;
using System.IO;

namespace EasySharpStandard.DiskIO.Serializers
{
    public static class StreamReaderExtensions
    {
        public static string ReadToEnd(this string filePath)
        {
            using (var sr = new StreamReader(filePath))
            {
                return sr.ReadToEnd();
            }
        }

        public static string[] ReadLines(
            this string filePath,
            StringSplitOptions splitOptions = StringSplitOptions.None)
        {
            return filePath.ReadToEnd().Split(new[] {Environment.NewLine}, splitOptions);
        }
    }
}
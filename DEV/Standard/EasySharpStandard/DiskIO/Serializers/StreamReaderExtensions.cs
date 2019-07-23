using System.Collections.Generic;
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

        public static IEnumerable<string> ReadLines(
            this string filePath,
            bool removeEmptyLine = false)
        {
            using (var sr = new StreamReader(filePath))
            {
                string line;
                while(null != (line = sr.ReadLine()))
                {
                    if (removeEmptyLine)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            yield return line;
                        }
                    }
                    else
                    {
                        yield return line;
                    }
                }
            }
        }
    }
}
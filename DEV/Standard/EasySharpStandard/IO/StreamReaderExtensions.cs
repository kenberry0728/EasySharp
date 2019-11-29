using System.Collections.Generic;
using System.IO;

namespace EasySharp.IO
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
            this IFilePath filePath,
            bool removeEmptyLine = false)
        {
            using (var sr = new StreamReader(filePath.Value))
            {
                string line;
                while (null != (line = sr.ReadLine()))
                {
                    if (removeEmptyLine)
                    {
                        if (!line.IsNullOrEmpty())
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
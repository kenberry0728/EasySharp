using System.Collections.Generic;
using System.IO;

namespace EasySharp.IO
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Extensions")]
    public static class StreamReaderExtensions
    {
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
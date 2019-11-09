using System.IO;

namespace EasySharp.IO
{
    public static class StreamWriterExtensions
    {
        public static void WriteToFile(this string content, string filePath)
        {
            using (var sw = new StreamWriter(filePath))
            {
                sw.Write(content);
            }
        }
    }
}
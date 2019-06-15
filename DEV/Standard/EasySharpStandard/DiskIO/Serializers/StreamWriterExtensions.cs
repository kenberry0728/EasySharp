using System.IO;

namespace EasySharpStandard.DiskIO.Serializers
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
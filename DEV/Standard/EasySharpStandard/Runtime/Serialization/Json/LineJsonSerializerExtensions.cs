using EasySharp.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace EasySharp.Runtime.Serialization.Json
{
    public static class LineJsonSerializerExtensions
    {
        public static void SerializeAsLinedJson<T>(this IEnumerable<T> instances, string filePath, params Type[] knownTypes)
        {
            var serializer = new DataContractJsonSerializer(typeof(T), knownTypes);
            filePath.EnsureDirectoryForFile();
            using (var streamWriter = new StreamWriter(filePath, false, Encoding.UTF8, 1024))
            {
                foreach (var instance in instances)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        serializer.WriteObject(memoryStream, instance);
                        var line = Encoding.UTF8.GetString(memoryStream.ToArray());
                        streamWriter.WriteLine(line);
                    }
                }
            }
        }

        public static IEnumerable<T> DeserializeFromLinedJson<T>(this string filePath, params Type[] knownTypes)
        {
            var serializer = new DataContractJsonSerializer(typeof(T), knownTypes);
            using (var streamReader = new StreamReader(filePath, Encoding.UTF8))
            {
                string line;
                while (null != (line = streamReader.ReadLine()))
                {
                    if (line == string.Empty)
                    {
                        continue;
                    }

                    using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(line)))
                    {
                        yield return (T)serializer.ReadObject(memoryStream);
                    }
                }
            }
        }
    }
}

using EasySharp.IO;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace EasySharp.Runtime.Serialization.Json
{
    public static class JsonSerializerExtensions
    {
        public static void SerializeAsJson(this object instance, string filePath, params Type[] knownTypes)
        {
            filePath.EnsureDirectoryForFile();
            var serializer = new DataContractJsonSerializer(instance.GetType(), knownTypes);
            using (var writer = new FileStream(filePath, FileMode.Create))
            {
                serializer.WriteObject(writer, instance);
            }
        }

        public static string GetSerializeJsonString(this object instance, params Type[] knownTypes)
        {
            var serializer = new DataContractJsonSerializer(instance.GetType(), knownTypes);
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, instance);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public static T DeserializeFromJson<T>(this string filePath, params Type[] knownTypes)
        {
            var serializer = new DataContractJsonSerializer(typeof(T), knownTypes);
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                return (T)serializer.ReadObject(stream);
            }
        }

        public static T DeserializeFromJsonString<T>(this string text, params Type[] knownTypes)
        {
            var serializer = new DataContractJsonSerializer(typeof(T), knownTypes);
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(text)))
            {
                return (T)serializer.ReadObject(stream);
            }
        }
    }
}

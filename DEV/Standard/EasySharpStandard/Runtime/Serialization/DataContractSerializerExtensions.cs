using EasySharp.IO;
using System;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace EasySharp.Runtime.Serialization
{
    public static class DataContractSerializerExtensions
    {
        public static void SerializeWithDataContract(this object instance, string filePath, DataContractSerializerSettings settings)
        {
            filePath.ToFilePath().EnsureDirectory();
            var serializer = new DataContractSerializer(instance.GetType(), settings);
            var xmlSettings = new XmlWriterSettings
            {
                NewLineHandling = NewLineHandling.Entitize,
                Indent = true,
                IndentChars = "    ",
                Encoding = new UTF8Encoding(false)
            };

            using (var writer = XmlWriter.Create(filePath, xmlSettings))
            {
                serializer.WriteObject(writer, instance);
            }
        }

        public static void SerializeWithDataContract(this object instance, string filePath, params Type[] knownTypes)
        {
            var settings = new DataContractSerializerSettings
            {
                KnownTypes = knownTypes
            };

            instance.SerializeWithDataContract(filePath, settings);
        }

        public static void SerializeWithDataContract(this object instance, string filePath, bool preserveObjectReferences, params Type[] knownTypes)
        {
            var settings = new DataContractSerializerSettings
            {
                KnownTypes = knownTypes,
                PreserveObjectReferences = preserveObjectReferences
            };

            instance.SerializeWithDataContract(filePath, settings);
        }

        public static T DeserializeWithDataContract<T>(this string filePath, params Type[] knownTypes)
        {
            var serializer = new DataContractSerializer(typeof(T), knownTypes);
            using (var reader = XmlReader.Create(filePath))
            {
                return (T)serializer.ReadObject(reader);
            }
        }

        public static object DeserializeWithDataContract(this string filePath, Type type, params Type[] knownTypes)
        {
            var serializer = new DataContractSerializer(type, knownTypes);
            using (var reader = XmlReader.Create(filePath))
            {
                return serializer.ReadObject(reader);
            }
        }
    }
}

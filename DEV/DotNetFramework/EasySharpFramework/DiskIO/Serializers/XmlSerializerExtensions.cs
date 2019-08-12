using System;
using System.Xml;
using System.Xml.Serialization;

namespace EasySharpStandard.DiskIO.Serializers
{
    public static class XmlSerializerExtensions
    {
        public static void SerializeAsXml(this object instance, string filePath)
        {
            var xmlWriterSettings = new XmlWriterSettings { Indent = true };
            var serializer = new XmlSerializer(instance.GetType());
            using (var xml = XmlWriter.Create(filePath, xmlWriterSettings))
            {
                serializer.Serialize(xml, instance);
            }
        }

        public static T DeserializeFromXml<T>(this string filePath)
        {
            return filePath.DeserializeFromXml<T>(typeof(T));
        }

        public static T DeserializeFromXml<T>(this string filePath, Type type)
        {
            var serializer = new XmlSerializer(type);
            using (var xml = XmlReader.Create(filePath))
            {
                return (T)serializer.Deserialize(xml);
            }
        }
    }
}

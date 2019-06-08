﻿using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace EasySharpStandard.DiskIO.Serializers
{
    public static class JsonSerializerExtensions
    {
        public static void SerializeAsJson(this object instance, string filePath, params Type[] knownTypes)
        {
            filePath.EnsureDirectory();
            var serializer = new DataContractJsonSerializer(instance.GetType(), knownTypes);
            using (var writer = new FileStream(filePath, FileMode.Create))
            {
                serializer.WriteObject(writer, instance);
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
    }
}

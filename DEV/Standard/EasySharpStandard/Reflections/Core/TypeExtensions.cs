using System;
using System.Linq;
using System.Reflection;

namespace EasySharpStandard.Reflections.Core
{
    public static class TypeExtensions
    {
        public static void CopyPropertyValues(this Type type, object from, object to, Func<PropertyInfo, bool> filter)
        {
            foreach (var property in type.GetProperties()
                                         .Where(filter)
                                         .Where(p => p.CanRead && p.CanWrite))
            {
                property.SetValue(to, property.GetValue(from));
            }
        }
    }
}

using System;
using System.Linq;
using System.Reflection;

namespace EasySharp.Reflection
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Extensions")]
    public static class TypeExtensions
    {
        public static object New(this Type type)
        {
            return Activator.CreateInstance(type);
        }

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

using System;
using System.Linq;
using System.Reflection;

namespace EasySharpStandard.Attributes.Core
{
    public static class ObjectExtensions
    {
        public static string ToCommaSeparatedString(this object instance, Func<PropertyInfo, bool> filter = null)
        {
            filter = filter ?? ((p) => true);
            return string.Join(
                    ", ",
                    instance.GetType().GetProperties()
                    .Where(filter)
                    .Select(p => $"{p.GetDisplayName()} : {GetDisplayValue(instance, p)}"));
        }

        private static object GetDisplayValue(object instance, PropertyInfo p)
        {
            // TODO :ENUM
            return p.GetValue(instance);
        }
    }
}

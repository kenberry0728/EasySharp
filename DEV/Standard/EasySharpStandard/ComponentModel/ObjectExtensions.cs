﻿using EasySharp.Reflection;
using System;
using System.Linq;
using System.Reflection;

namespace EasySharp.ComponentModel
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

        public static object GetDisplayValue(object instance, PropertyInfo p)
        {
            if (p.PropertyType.IsEnum)
            {
                return p.PropertyType.GetEnumDisplayValue(p.GetValue(instance));
            }
            else
            {
                return p.GetValue(instance);
            }
        }
    }
}

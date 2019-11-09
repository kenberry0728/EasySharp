﻿using System;
using System.Reflection;

namespace EasySharp.Reflections.Core
{
    public static class CustomAttributeExtensions
    {
        public static bool HasCustomAttribute<T>(this MemberInfo element) where T : Attribute
        {
            return element.GetCustomAttribute<T>() != null;
        }
    }
}

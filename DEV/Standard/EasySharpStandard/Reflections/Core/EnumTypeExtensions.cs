﻿using EasySharp.ComponentModel;
using System;

namespace EasySharp.Reflections.Core
{
    public static class EnumTypeExtensions
    {
        public static string GetEnumDisplayValue(this Type enumType, object enumValue)
        {
            // TODO: Debug assert
            return enumType.GetField(enumValue.ToString()).GetDisplayName();
        }
    }
}

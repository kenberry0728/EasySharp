using System;

namespace EasySharp.ComponentModel.Reflection
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Extensions")]
    public static class EnumTypeExtensions
    {
        public static string GetEnumDisplayValue(this Type enumType, object enumValue)
        {
            // TODO: Debug assert
            return enumType.GetField(enumValue.ToString()).GetDisplayName();
        }
    }
}

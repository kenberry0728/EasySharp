using System;

namespace EasySharp
{
    public static class StringExtensions
    {
        public static string ToEmptyIfNull(this string argument)
        {
            if (argument == null)
            {
                return string.Empty;
            }
            else
            {
                return argument;
            }
        }

        public static string Indent(this string target, int level)
        {
            return new string('\t', level) + target;
        }

        public static bool IsNullOrEmpty(this string instance)
        {
            return string.IsNullOrEmpty(instance);
        }

        public static bool OrdinalStartsWith(this string target, string value)
        {
            return target?.StartsWith(value, StringComparison.Ordinal) == true; 
        }

        public static bool OrdinalEndsWith(this string target, string value)
        {
            return target?.EndsWith(value, StringComparison.Ordinal) == true;
        }

        public static bool OrdinalEquals(this string target, string value)
        {
            return string.Equals(target, value, StringComparison.Ordinal);
        }

        public static bool OrdinalIgnoreCaseEquals(this string target, string value)
        {
            return string.Equals(target, value, StringComparison.Ordinal);
        }

        public static T EnumParse<T>(this string enumValue)
            where T : struct
        {
            return (T)Enum.Parse(typeof(T), enumValue, true);
        }
    }
}
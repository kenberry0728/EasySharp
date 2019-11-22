﻿using System;

namespace EasySharp
{
    public static class StringExtensions
    {
        public static void ThrowArgumentNullOrEmptyException(this string argument, string argumentName)
        {
            if (argument.IsNullOrEmpty())
            {
                throw new ArgumentException(argumentName);
            }
        }

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
            return target.StartsWith(value, StringComparison.Ordinal);
        }

        public static bool OrdinalEquals(this string target, string value)
        {
            return target.Equals(value, StringComparison.Ordinal);
        }
    }
}
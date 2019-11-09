﻿using System;

namespace EasySharp
{
    public static class StringExtensions
    {
        public static void ThrowArgumentNullOrEmptyException(this string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
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
    }
}
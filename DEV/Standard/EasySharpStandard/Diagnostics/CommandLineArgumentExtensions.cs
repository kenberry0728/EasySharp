using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EasySharp.Processes
{
    public static class CommandLineArgumentExtensions
    {
        private static readonly Regex commandLineEscapePattern = new Regex("(\\\\*)\"");
        private static readonly Regex lastBackSlashPattern = new Regex(@"(\\+)$");

        public static string ToCommandLineValue(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            var containsSpace = value.IndexOfAny(new[] { ' ', '\t' }) != -1;
            value = commandLineEscapePattern.Replace(value, @"$1\$&");

            if (containsSpace)
            {
                value = "\"" + lastBackSlashPattern.Replace(value, "$1$1") + "\"";
            }

            return value;
        }

        public static string EncodeCommandLineValues(this IEnumerable<string> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            return string.Join(" ", values.Select(ToCommandLineValue));
        }
    }
}
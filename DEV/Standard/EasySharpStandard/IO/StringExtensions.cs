using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EasySharp.IO
{
    public static class StringExtensions
    {
        private static readonly HashSet<char> invalidFileNameChars
            = new HashSet<char>(Path.GetInvalidFileNameChars().Except(new[] { '\\', ':' , '/' }));

        public static bool ContainsInvalidFileNameChars(this string value)
        {
            return value.Any(v => invalidFileNameChars.Contains(v));
        }

        public static void ThrowArgumentExceptionIfContainsInvalidFileNameChars(this string value, string parameterName)
        {
            if (value.ContainsInvalidFileNameChars())
            {
                throw new ArgumentException(parameterName);
            }
        }

        public static IDirectoryPath ToDirectoryPath(this string directoryPath)
        {
            return DirectoryPath.Create(directoryPath);
        }

        public static IFilePath ToFilePath(this string filePath)
        {
            return FilePath.Create(filePath);
        }
    }
}

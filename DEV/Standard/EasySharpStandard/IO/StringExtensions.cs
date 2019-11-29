using System;
using System.IO;

namespace EasySharp.IO
{
    public static class StringExtensions
    {
        public static bool ContainsInvalidFileNameChars(this string value)
        {
            return 0 <= value.IndexOfAny(Path.GetInvalidFileNameChars());
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

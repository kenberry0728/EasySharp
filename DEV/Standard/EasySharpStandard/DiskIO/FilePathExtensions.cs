using System;
using System.Collections.Generic;
using System.IO;

namespace EasySharpStandard.DiskIO
{
    public static class PathExtensions
    {
        #region Public Methods

        public static string GetRelativePath(this string fullPath, string relativeDirectoryPath)
        {
            var relativeDirectoryUri = new Uri(relativeDirectoryPath);
            var fullUri = new Uri(fullPath);

            //絶対Uriから相対Uriを取得する
            var relativeUri = relativeDirectoryUri.MakeRelativeUri(fullUri);
            //文字列に変換する
            return relativeUri.ToString();
        }

        public static string ToFullFileName(this string filePath)
        {
            return new FileInfo(filePath).FullName;
        }

        public static void EnsureDirectoryForFile(this string filePath)
        {
            var directoryPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                directoryPath.CreateDirectoryRecursively();
            }
        }

        #endregion

    }
}
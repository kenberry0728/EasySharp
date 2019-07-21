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
            if (!relativeDirectoryPath.EndsWith(@"\"))
            {
                relativeDirectoryPath += @"\";
            }

            var relativeDirectoryUri = new Uri(relativeDirectoryPath);
            var fullUri = new Uri(fullPath);

            //���Uri���瑊��Uri���擾����
            var relativeUri = relativeDirectoryUri.MakeRelativeUri(fullUri);
            //������ɕϊ�����
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
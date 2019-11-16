using System;
using System.IO;

namespace EasySharp.IO
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
            return relativeUri.ToString().Replace(@"/", @"\");
        }

        public static string ToFullFileName(this string filePath)
        {
            return new FileInfo(filePath).FullName;
        }

        public static void EnsureDirectoryForFile(this string filePath)
        {
            var directoryPath = Path.GetDirectoryName(filePath);
            if (!directoryPath.IsNullOrEmpty() && !Directory.Exists(directoryPath))
            {
                directoryPath.CreateDirectoryRecursively();
            }
        }

        public static void CopyFile(this string filePath, string targetFilePath, bool overwrite = true, bool ensureDirectoryForFile = true)
        {
            if (ensureDirectoryForFile)
            {
                targetFilePath.EnsureDirectoryForFile();
            }

            File.Copy(filePath, targetFilePath, overwrite);
        }

        #endregion
    }
}
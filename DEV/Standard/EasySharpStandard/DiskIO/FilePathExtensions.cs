using System;
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

        public static void EnsureDirectoryForFile(this string filePath)
        {
            var directoryPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                directoryPath.CreateDirectoryRecursively();
            }
        }

        public static void CreateDirectoryRecursively(this string directoryPath)
        {
            directoryPath.CreateDirectoryRecursivelyInner();
        }

        public static void CopyDirectory(
            this string sourceDirName, 
            string destDirName, 
            bool overwrite = true,
            bool copySubDirs = true,
            Func<FileInfo, bool> isExcludedFile = null)
        {
            var dir = new DirectoryInfo(sourceDirName);
            var dirs = dir.GetDirectories();
            destDirName.CreateDirectoryRecursively();

            foreach (var file in dir.GetFiles())
            {
                if (isExcludedFile != null && isExcludedFile(file))
                {
                    continue;
                }

                file.CopyTo(Path.Combine(destDirName, file.Name), overwrite);
            }

            if (copySubDirs)
            {
                foreach (var subDirectory in dirs)
                {
                    var tempPath = Path.Combine(destDirName, subDirectory.Name);
                    CopyDirectory(subDirectory.FullName, tempPath, overwrite, true, isExcludedFile);
                }
            }
        }

        #endregion

        #region Private Methods

        private static DirectoryInfo CreateDirectoryRecursivelyInner(this string directoryPath)
        {
            directoryPath = string.IsNullOrEmpty(directoryPath)
                ? Directory.GetCurrentDirectory() 
                : directoryPath;
            var parentDirectoryInfo = Directory.GetParent(directoryPath);
            if (!Directory.Exists(parentDirectoryInfo.FullName))
            {
                parentDirectoryInfo.FullName.CreateDirectoryRecursively();
            }

            return Directory.CreateDirectory(directoryPath);
        }

        #endregion
    }
}
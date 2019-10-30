using System;
using System.Collections.Generic;
using System.IO;

namespace EasySharpStandard.DiskIO
{
    public static class DirectoryPathExtention
    {
        #region Public Methods

        public static string ToFullDirectoryName(this string directoryPath)
        {
            return string.IsNullOrEmpty(directoryPath) ? Directory.GetCurrentDirectory() : new DirectoryInfo(directoryPath).FullName;
        }

        public static void CreateDirectoryRecursively(this string directoryPath)
        {
            directoryPath.CreateDirectoryRecursivelyInner();
        }

        public static void DeleteDirectoryRecursively(this string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }

        public static void CopyDirectory(
            this string sourceDirectoryPath,
            string destDirectoryPath,
            bool overwrite = true,
            bool copySubDirs = true,
            ICollection<string> excludeFileRelativePaths = null)
        {
            var dir = new DirectoryInfo(sourceDirectoryPath);
            var dirs = dir.GetDirectories();
            destDirectoryPath.CreateDirectoryRecursively();

            foreach (var file in dir.GetFiles())
            {
                if (excludeFileRelativePaths?.Contains(file.FullName.GetRelativePath(dir.FullName)) == true)
                {
                    continue;
                }

                file.CopyTo(Path.Combine(destDirectoryPath, file.Name), overwrite);
            }

            if (copySubDirs)
            {
                foreach (var subDirectory in dirs)
                {
                    var tempPath = Path.Combine(destDirectoryPath, subDirectory.Name);
                    CopyDirectory(subDirectory.FullName, tempPath, overwrite, true, excludeFileRelativePaths);
                }
            }
        }

        public static void SetLastWriteTimeToAllFiles(this string directoryPath, DateTime lastWriteTimeToSet)
        {
            var allfiles = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
            foreach (var file in allfiles)
            {
                File.SetLastWriteTime(file, lastWriteTimeToSet);
            }
        }
        
        public static void TemporarySetCurrentDirectory(this string directoryPath, Action action)
        {
            var previousCurrentPath = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(directory);

            action();

            Directory.SetCurrentDirectory(previousCurrentPath);
        }

        #endregion

        #region Private Methods

        private static void CreateDirectoryRecursivelyInner(this string directoryPath)
        {
            directoryPath = string.IsNullOrEmpty(directoryPath)
                ? Directory.GetCurrentDirectory()
                : directoryPath;
            var parentDirectoryInfo = Directory.GetParent(directoryPath);
            if (!Directory.Exists(parentDirectoryInfo.FullName))
            {
                parentDirectoryInfo.FullName.CreateDirectoryRecursively();
            }

            Directory.CreateDirectory(directoryPath);
        }

        #endregion
    }
}

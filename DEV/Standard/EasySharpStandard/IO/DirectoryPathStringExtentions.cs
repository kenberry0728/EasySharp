using System;
using System.Collections.Generic;
using System.IO;

namespace EasySharp.IO
{
    public static class DirectoryPathStringExtentions
    {
        #region Public Methods

        [Obsolete]
        public static string ToFullDirectoryName(this string directoryPath)
        {
            if (directoryPath.IsNullOrEmpty())
            {
                return Directory.GetCurrentDirectory();
            }
            else if(Path.IsPathRooted(directoryPath))
            {
                return directoryPath;
            }
            else
            {
                return new DirectoryInfo(directoryPath).FullName;
            }
        }

        [Obsolete]
        public static void CreateDirectoryRecursively(this string directoryPath)
        {
            directoryPath.CreateDirectoryRecursivelyInner();
        }

        [Obsolete]
        public static void DeleteDirectoryRecursively(this string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }

        [Obsolete]
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
                    subDirectory.FullName.CopyDirectory(tempPath, overwrite, true, excludeFileRelativePaths);
                }
            }
        }

        [Obsolete]
        public static void SetLastWriteTimeToAllFiles(this string directoryPath, DateTime lastWriteTimeToSet)
        {
            var allfiles = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
            foreach (var file in allfiles)
            {
                File.SetLastWriteTime(file, lastWriteTimeToSet);
            }
        }

        [Obsolete]
        public static void TemporarySetCurrentDirectory(this string directoryPath, Action action)
        {
            var previousCurrentPath = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(directoryPath);
            Try.Finally(
                action,
                () => { Directory.SetCurrentDirectory(previousCurrentPath); });
        }

        #endregion

        #region Private Methods

        private static void CreateDirectoryRecursivelyInner(this string directoryPath)
        {
            directoryPath = directoryPath.IsNullOrEmpty()
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

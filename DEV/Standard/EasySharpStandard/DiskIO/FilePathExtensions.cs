using System.IO;

namespace EasySharpStandard.DiskIO
{
    public static class PathExtensions
    {
        #region Public Methods

        public static void EnsureDirectory(this string filePath)
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

        #endregion

        #region Private Methods

        private static DirectoryInfo CreateDirectoryRecursivelyInner(this string directoryPath)
        {
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
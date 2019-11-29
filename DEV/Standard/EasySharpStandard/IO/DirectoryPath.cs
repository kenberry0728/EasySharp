using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EasySharp.IO
{
    public class DirectoryPath : PathObjectBase, IDirectoryPath
    {
        public delegate IDirectoryPath CreateDirectoryPath(string value);

        public static IDirectoryPath Create(string value)
        {
            value.ThrowExceptionIfNull(nameof(value));
            value.ThrowArgumentExceptionIfContainsInvalidFileNameChars(nameof(value));

            return new DirectoryPath(value);
        }

        private DirectoryPath(string value)
            : base(value)
        {
        }

        public override bool Equals(object obj)
        {
            return obj is DirectoryPath && base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public DirectoryPath ToFullDirectoryPath()
        {
            return new DirectoryPath(this.Value.ToFullDirectoryPath());
        }

        public void CreateDirectoryRecursively()
        {
            this.Value.CreateDirectoryRecursively();
        }

        public void DeleteDirectoryRecursively()
        {
            this.Value.DeleteDirectoryRecursively();
        }

        public void CopyDirectory(
            IDirectoryPath directoryPath,
            bool overwrite = true,
            bool copySubDirs = true,
            ICollection<string> excludeFileRelativePaths = null)
        {
            this.Value.CopyDirectory(directoryPath.Value, overwrite, copySubDirs, excludeFileRelativePaths);
        }

        public void SetLastWriteTimeToAllFiles(DateTime lastWriteTime)
        {
            this.Value.SetLastWriteTimeToAllFiles(lastWriteTime);
        }

        public void ScopedSetCurrentDirectory(Action action)
        {
            this.Value.TemporarySetCurrentDirectory(action);
        }

        public IEnumerable<string> GetFiles(string searchPattern, SearchOption searchOption)
        {
            var directoryInfo = new DirectoryInfo(this.Value);
            return directoryInfo.GetFiles(searchPattern, searchOption).Select(f => f.FullName);
        }
    }
}

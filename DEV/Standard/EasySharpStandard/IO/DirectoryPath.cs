using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EasySharp.IO
{
    public class DirectoryPath : ValueObjectBase<string>, IDirectoryPath
    {
        public delegate IDirectoryPath CreateDirectoryPath(string value);

        public static IDirectoryPath Create(string value)
        {
            value.ThrowExceptionIfNull(nameof(value));
            var invalidChars = Path.GetInvalidFileNameChars();
            if (0 <= value.IndexOfAny(invalidChars))
            {
                throw new ArgumentException(nameof(value));
            }

            return new DirectoryPath(value);
        }

        private DirectoryPath(string value)
            : base(value)
        {
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DirectoryPath directoryPath))
            {
                return false;
            }

            // TODO: どこまで高機能にするかは考え物
            return this.Value.OrdinalEquals(directoryPath.Value);
        }

        public override int GetHashCode()
        {
            return this.Value.ToUpperInvariant().GetHashCode();
        }

        public override string ToString()
        {
            return this.Value;
        }

        public bool IsAbolutePath => Path.IsPathRooted(this.Value);

        public DirectoryPath ToFullDirectoryPath()
        {
            return new DirectoryPath(this.Value.ToFullDirectoryName());
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
            DirectoryPath directoryPath,
            bool overwrite = true,
            bool copySubDirs = true)
        {
            this.Value.CopyDirectory(directoryPath.Value, overwrite, copySubDirs);
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EasySharp.IO
{
    public class DirectoryPath : PathObjectBase, IDirectoryPath
    {
        #region Constructor

        private DirectoryPath(string value)
            : base(value)
        {
        }

        #endregion

        #region Factory Method

        public static IDirectoryPath Create(string value)
        {
            value.ThrowArgumentExceptionIfNull(nameof(value));
            value.ThrowArgumentExceptionIfContainsInvalidFileNameChars(nameof(value));

            return new DirectoryPath(value);
        }

        #endregion

        #region Public Methods

        public override bool Equals(object obj)
        {
            return obj is DirectoryPath && base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public IDirectoryPath ToFullDirectoryPath()
        {
            if (this.Value.IsNullOrEmpty())
            {
                return Directory.GetCurrentDirectory().ToDirectoryPath();
            }
            else if (this.IsAbolutePath)
            {
                return this;
            }
            else
            {
                return new DirectoryInfo(this.Value).FullName.ToDirectoryPath();
            }
        }

        public IEnumerable<IFilePath> GetFilePaths(string searchPattern, SearchOption searchOption)
        {
            var directoryInfo = new DirectoryInfo(this.Value);
            return directoryInfo
                .GetFiles(searchPattern, searchOption)
                .Select(fi => fi.FullName.ToFilePath());
        }

        public void CreateDirectoryRecursively()
        {
            var directoryPath = this.Value.IsNullOrEmpty()
                ? Directory.GetCurrentDirectory()
                : this.Value;
            var parentDirectoryInfo = Directory.GetParent(directoryPath);
            if (!Directory.Exists(parentDirectoryInfo.FullName))
            {
                parentDirectoryInfo.FullName.ToDirectoryPath().CreateDirectoryRecursively();
            }

            Directory.CreateDirectory(directoryPath);
        }

        public void DeleteDirectory()
        {
            if (Directory.Exists(this.Value))
            {
                Directory.Delete(this.Value, true);
            }
        }

        public void CopyDirectory(
            IDirectoryPath directoryPath,
            bool overwrite = true,
            bool copySubDirs = true,
            ICollection<string> excludeFileRelativePaths = null)
        {
            var dir = new DirectoryInfo(this.Value);
            var dirs = dir.GetDirectories();
            this.CreateDirectoryRecursively();

            foreach (var file in dir.GetFiles())
            {
                if (excludeFileRelativePaths?.Contains(
                    file.FullName.ToFilePath().GetRelativePath(
                        dir.FullName.ToDirectoryPath()).Value) == true)
                {
                    continue;
                }

                file.CopyTo(Path.Combine(this.Value, file.Name), overwrite);
            }

            if (copySubDirs)
            {
                foreach (var subDirectory in dirs)
                {
                    var tempPath = Path.Combine(this.Value, subDirectory.Name);
                    subDirectory.FullName.ToDirectoryPath().CopyDirectory(tempPath.ToDirectoryPath(), overwrite, true, excludeFileRelativePaths);
                }
            }
        }

        public void SetLastWriteTimeToAllFiles(DateTime lastWriteTime)
        {
            var allFiles = Directory.GetFiles(this.Value, "*", SearchOption.AllDirectories);
            foreach (var file in allFiles)
            {
                File.SetLastWriteTime(file, lastWriteTime);
            }
        }

        public void ScopedSetCurrentDirectory(Action action)
        {
            var previousCurrentPath = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(this.Value);
            Try.Finally(
                action,
                () => { Directory.SetCurrentDirectory(previousCurrentPath); });
        }

        public IEnumerable<string> GetFiles(string searchPattern, SearchOption searchOption)
        {
            var directoryInfo = new DirectoryInfo(this.Value);
            return directoryInfo.GetFiles(searchPattern, searchOption).Select(f => f.FullName);
        }

        public override bool Exists()
        {
            return Directory.Exists(this.Value);
        }

        #endregion
    }
}

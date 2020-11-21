using System;
using System.Collections.Generic;
using System.IO;

namespace EasySharp.IO
{
    public class FilePath : PathObjectBase, IFilePath
    {
        #region Constructor

        private FilePath(string value)
        : base(value)
        {
        }

        #endregion

        #region Factory Method

        public static FilePath Create(string value)
        {
            value.ThrowArgumentExceptionIfNull(nameof(value));
            value.ThrowArgumentExceptionIfContainsInvalidFileNameChars(nameof(value));

            return new FilePath(value);
        }

        #endregion

        #region Public Methods

        public override bool Equals(object obj)
        {
            return obj is FilePath && base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public DateTime GetLastWriteTimeUtc()
        {
            return new FileInfo(this.Value).LastWriteTimeUtc;
        }

        public IFilePath GetRelativePath(IDirectoryPath relativeDirectoryPath)
        {
            if (!this.IsAbolutePath)
            {
                return this;
            }

            relativeDirectoryPath.ThrowArgumentExceptionIfNull(nameof(relativeDirectoryPath));
            if (!relativeDirectoryPath.Value.OrdinalEndsWith(@"\"))
            {
                relativeDirectoryPath = (relativeDirectoryPath.Value + @"\").ToDirectoryPath();
            }

            var relativeDirectoryUri = new Uri(relativeDirectoryPath.Value);
            var fullUri = new Uri(this.Value);

            // 絶対Uriから相対Uriを取得する
            var relativeUri = relativeDirectoryUri.MakeRelativeUri(fullUri);
            // 文字列に変換する
            return relativeUri.ToString().Replace(@"/", @"\").ToFilePath();
        }

        public IFilePath ToFullPath()
        {
            return new FileInfo(this.Value).FullName.ToFilePath();
        }

        public void EnsureDirectory()
        {
            var directoryPath = Path.GetDirectoryName(this.Value);
            if (!directoryPath.IsNullOrEmpty() && !Directory.Exists(directoryPath))
            {
                directoryPath.ToDirectoryPath().CreateDirectoryRecursively();
            }
        }

        public void Copy(IFilePath targetFilePath, bool overwrite = true, bool ensureDirectoryForFile = true)
        {
            targetFilePath.ThrowArgumentExceptionIfNull(nameof(targetFilePath));

            if (ensureDirectoryForFile)
            {
                targetFilePath.EnsureDirectory();
            }

            File.Copy(this.Value, targetFilePath.Value, overwrite);
        }

        public override bool Exists()
        {
            return File.Exists(this.Value);
        }

        public string ReadAllText()
        {
            return File.ReadAllText(this.Value);
        }

        public IEnumerable<string> ReadLines(bool removeEmptyLine = false)
        {
            using (var sr = new StreamReader(this.Value))
            {
                string line;
                while (null != (line = sr.ReadLine()))
                {
                    if (removeEmptyLine)
                    {
                        if (!line.IsNullOrEmpty())
                        {
                            yield return line;
                        }
                    }
                    else
                    {
                        yield return line;
                    }
                }
            }
        }


        #endregion
    }
}

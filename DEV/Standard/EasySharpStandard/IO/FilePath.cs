using System;
using System.IO;

namespace EasySharp.IO
{
    public class FilePath : PathObjectBase, IFilePath
    {
        public delegate FilePath CreateFilePath(string value);

        public static FilePath Create(string value)
        {
            value.ThrowExceptionIfNull(nameof(value));
            value.ThrowArgumentExceptionIfContainsInvalidFileNameChars(nameof(value));

            return new FilePath(value);
        }

        private FilePath(string value)
            : base(value)
        {
        }

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
    }
}

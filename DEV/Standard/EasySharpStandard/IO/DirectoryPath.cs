using System;
using System.IO;

namespace EasySharp.IO
{
    public class DirectoryPath : ValueObjectBase<string>
    {
        public static DirectoryPath Create(string value)
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
    }
}

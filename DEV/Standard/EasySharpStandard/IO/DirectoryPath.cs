using System;
using System.IO;

namespace EasySharp.IO
{
    public class DirectoryPath : ValueObjectBase<string>
    {
        public DirectoryPath(string value)
            :base(value)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            if (0 <= this.Value.IndexOfAny(invalidChars))
            {
                throw new ArgumentException(nameof(value));
            }
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
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}

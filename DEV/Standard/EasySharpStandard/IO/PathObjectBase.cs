﻿using System.IO;

namespace EasySharp.IO
{
    public abstract class PathObjectBase : ValueObjectBase<string>, IPathObjectBase
    {
        protected PathObjectBase(string value)
            : base(value)
        {
        }

        public override bool Equals(object obj)
        {
            return (obj is PathObjectBase pathObject)
                && this.Value.OrdinalEquals(pathObject.Value);
        }

        public override string ToString()
        {
            return this.Value;
        }

        public bool IsAbsolutePath => Path.IsPathRooted(this.Value);

        public override int GetHashCode()
        {
            return this.Value.ToUpperInvariant().GetHashCode();
        }

        public abstract bool Exists();
    }
}

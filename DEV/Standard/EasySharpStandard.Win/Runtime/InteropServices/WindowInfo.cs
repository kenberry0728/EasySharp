using EasySharp.Reflection;
using System;

namespace EasySharp.Win.Runtime.InteropServices
{
    public struct WindowInfo : IEquatable<WindowInfo>
    {
        public WindowInfo(IntPtr handle, string title)
        {
            this.Handle = handle;
            this.Title = title;
        }

        public IntPtr Handle { get; }

        public string Title { get; }

        public override string ToString()
        {
            return this.ToFormattedString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(WindowInfo left, WindowInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WindowInfo left, WindowInfo right)
        {
            return !(left == right);
        }

        public bool Equals(WindowInfo other)
        {
            return other != null
                && this.Handle == other.Handle
                && this.Title.OrdinalEquals(other.Title);
        }
    }
}

using EasySharp.IO;
using EasySharp.Reflection;
using System;

namespace EasySharp.Win.Runtime.InteropServices
{
    public struct WindowInfo : IEquatable<WindowInfo>
    {
        public WindowInfo(IntPtr handle, string title, int processId, int threadId)
        {
            this.Handle = handle;
            this.Title = title;
            this.ProcessId = processId;
            this.ThreadId = threadId;
        }

        public IntPtr Handle { get; }
        public string Title { get; }
        public int ProcessId { get; }
        public int ThreadId { get; }

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
                && this.Title.OrdinalEquals(other.Title)
                && this.ProcessId == other.ProcessId
                && this.ThreadId == other.ThreadId;
        }
    }
}

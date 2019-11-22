using EasySharp.Reflection;
using System;

namespace EasySharp.Win.Runtime.InteropServices
{
    public struct WindowInfo
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
    }
}

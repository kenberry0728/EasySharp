using System;

namespace EasySharp.Win.WindowHandlers
{
    public class WindowInfo
    {
        public WindowInfo(IntPtr handle, string title)
        {
            this.Handle = handle;
            this.Title = title;
        }
        public IntPtr Handle { get; }

        public string Title { get;  }
    }
}

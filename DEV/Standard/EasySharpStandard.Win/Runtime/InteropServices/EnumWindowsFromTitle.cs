using EasySharp.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EasySharp.Win.Runtime.InteropServices
{
    public sealed class EnumWindowsFromTitle
    {
        private class EnumWindowsFromTitleService
        {
            private delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lparam);

            private static class NativeMethods
            {
                [DllImport("user32.dll")]
                [return: MarshalAs(UnmanagedType.Bool)]
                public extern static bool EnumWindows(EnumWindowsDelegate lpEnumFunc, IntPtr lparam);

                [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
                public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

                [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
                public static extern int GetWindowTextLength(IntPtr hWnd);
            }

            private readonly List<WindowInfo> windowInfos = new List<WindowInfo>();
            private readonly Func<string, bool> windowTitlePredicate;

            internal EnumWindowsFromTitleService(Func<string, bool> windowTitlePredicate)
            {
                this.windowTitlePredicate = windowTitlePredicate;
            }

            public IReadOnlyCollection<WindowInfo> GetWindowInfos()
            {
                NativeMethods.EnumWindows(new EnumWindowsDelegate(this.EnumWindowCallBack), IntPtr.Zero);
                return this.windowInfos;
            }

            private bool EnumWindowCallBack(IntPtr hWnd, IntPtr lparam)
            {
                int textLength = NativeMethods.GetWindowTextLength(hWnd);
                if (0 < textLength)
                {
                    var windowTitleStringBuilder = new StringBuilder(textLength + 1);
                    NativeMethods.GetWindowText(hWnd, windowTitleStringBuilder, windowTitleStringBuilder.Capacity);

                    var windowTitle = windowTitleStringBuilder.ToString();
                    if (this.windowTitlePredicate(windowTitle))
                    {
                        this.windowInfos.Add(new WindowInfo(hWnd, windowTitle));
                    }
                }

                return true; //すべてのウィンドウを列挙する
            }
        }

        public static bool TryGetWindowHandleFromTitle(
            Func<string, bool> titlePredicate,
            out IntPtr windowHandler,
            int maxRetry = 60,
            int intervalMilliseconds = 1000)
        {
            return Retry.Until(
                () => GetWindowHandlesFromTitle(titlePredicate).FirstOrDefault(),
                (lw) => !lw.IsDefaultStructValue(),
                out windowHandler,
                maxRetry,
                intervalMilliseconds);
        }

        public static IEnumerable<IntPtr> GetWindowHandlesFromTitle(Func<string, bool> titlePredicate)
        {
            return GetWindowInfos(titlePredicate).Select(wi => wi.Handle);
        }

        public static IReadOnlyCollection<WindowInfo> GetWindowInfos(Func<string, bool> titlePredicate = null)
        {
            titlePredicate = titlePredicate.TrueIfNull();
            var service = new EnumWindowsFromTitleService(titlePredicate);
            return service.GetWindowInfos();
        }
    }
}

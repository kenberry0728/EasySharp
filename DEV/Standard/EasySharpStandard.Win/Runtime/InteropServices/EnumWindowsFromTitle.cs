using EasySharp.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EasySharp.Win.Runtime.InteropServices
{
    public class EnumWindowsFromTitle
    {
        private class EnumWindowsFromTitleService
        {
            private delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lparam);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private extern static bool EnumWindows(EnumWindowsDelegate lpEnumFunc, IntPtr lparam);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern int GetWindowText(IntPtr hWnd,
                StringBuilder lpString, int nMaxCount);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern int GetWindowTextLength(IntPtr hWnd);

            private readonly List<WindowInfo> windowInfos = new List<WindowInfo>();
            private readonly Func<string, bool> windowTitlePredicate;

            internal EnumWindowsFromTitleService(Func<string, bool> windowTitlePredicate)
            {
                this.windowTitlePredicate = windowTitlePredicate;
            }

            public IEnumerable<WindowInfo> GetWindowInfos()
            {
                EnumWindows(new EnumWindowsDelegate(this.EnumWindowCallBack), IntPtr.Zero);
                return this.windowInfos;
            }

            private bool EnumWindowCallBack(IntPtr hWnd, IntPtr lparam)
            {
                int textLen = GetWindowTextLength(hWnd);
                if (0 < textLen)
                {
                    var windowTitleStringBuilder = new StringBuilder(textLen + 1);
                    GetWindowText(hWnd, windowTitleStringBuilder, windowTitleStringBuilder.Capacity);

                    var windowTitle = windowTitleStringBuilder.ToString();
                    if (this.windowTitlePredicate(windowTitle))
                    {
                        this.windowInfos.Add(new WindowInfo(hWnd, windowTitle));
                    }
                }

                return true; //すべてのウィンドウを列挙する
            }
        }

        public static IEnumerable<IntPtr> GetWindowHandlesFromTitle(string windowTitle)
        {
            return GetWindowHandlesFromTitle((title) => title == windowTitle);
        }

        public static bool TryGetWindowHandleFromTitle(
            string windowTitle,
            out IntPtr windowHandler,
            int maxRetry = 60,
            int intervalMilliseconds = 1000)
        {
            return TryGetWindowHandleFromTitle(
                s => s == windowTitle,
                out windowHandler,
                maxRetry,
                intervalMilliseconds);
        }

        public static bool TryGetWindowHandleFromTitle(
            Func<string, bool> titlePredicate,
            out IntPtr windowHandler,
            int maxRetry = 60,
            int intervalMilliseconds = 1000)
        {
            windowHandler = Retry.Until(
                () => GetWindowHandlesFromTitle(titlePredicate).FirstOrDefault(),
                (lw) => !lw.IsDefaultStructValue(),
                maxRetry,
                intervalMilliseconds);
            if (windowHandler.IsDefaultStructValue())
            {
                return false;
            }

            return true;
        }

        public static IEnumerable<IntPtr> GetWindowHandlesFromTitle(Func<string, bool> titlePredicate)
        {
            return GetWindowInfos(titlePredicate).Select(wi => wi.Handle);
        }

        public static IEnumerable<WindowInfo> GetWindowInfos(Func<string, bool> titlePredicate = null)
        {
            titlePredicate = titlePredicate ?? (s => true);
            var service = new EnumWindowsFromTitleService(titlePredicate);
            return service.GetWindowInfos();
        }
    }
}

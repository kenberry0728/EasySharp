using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace EasySharpStandard.Win.WindowHandlers
{
    public class EnumWindowsFromTitleService
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

        private readonly List<IntPtr> windowHandlers = new List<IntPtr>();
        private readonly Func<string, bool> windowTitlePredicate;

        private EnumWindowsFromTitleService(Func<string, bool> windowTitlePredicate)
        {
            this.windowTitlePredicate = windowTitlePredicate;
        }

        public static IEnumerable<IntPtr> GetWindowHandlersFromTitle(string windowTitle)
        {
            return GetWindowHandlersFromTitle((title) => title == windowTitle);
        }

        public static IEnumerable<IntPtr> GetWindowHandlersFromTitle(Func<string, bool> titlePredicate)
        {
            var instance = new EnumWindowsFromTitleService(titlePredicate);
            EnumWindows(new EnumWindowsDelegate(instance.EnumWindowCallBack), IntPtr.Zero);
            return instance.windowHandlers;
        }

        private bool EnumWindowCallBack(IntPtr hWnd, IntPtr lparam)
        {
            int textLen = GetWindowTextLength(hWnd);
            if (0 < textLen)
            {
                var windowTitle = new StringBuilder(textLen + 1);
                GetWindowText(hWnd, windowTitle, windowTitle.Capacity);

                if (this.windowTitlePredicate(windowTitle.ToString()))
                {
                    this.windowHandlers.Add(hWnd);
                }
            }

            return true; //すべてのウィンドウを列挙する
        }
    }
}

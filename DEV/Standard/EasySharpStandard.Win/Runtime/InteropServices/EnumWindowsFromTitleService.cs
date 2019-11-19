﻿using EasySharp;
using EasySharp.Threading;
using EasySharp.Win.WindowHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EasySharp.Win.WindowHandle
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

        private readonly List<WindowInfo> windowInfos = new List<WindowInfo>();
        private readonly Func<string, bool> windowTitlePredicate;

        private EnumWindowsFromTitleService(Func<string, bool> windowTitlePredicate)
        {
            this.windowTitlePredicate = windowTitlePredicate;
        }

        public static IEnumerable<IntPtr> GetWindowHandlesFromTitle(string windowTitle)
        {
            return GetWindowHandlesFromTitle((title) => title == windowTitle);
        }

        public static IEnumerable<IntPtr> GetWindowHandlesFromTitle(Func<string, bool> titlePredicate)
        {
            var instance = new EnumWindowsFromTitleService(titlePredicate);
            EnumWindows(new EnumWindowsDelegate(instance.EnumWindowCallBack), IntPtr.Zero);
            return instance.windowInfos.Select(wi => wi.Handle);
        }

        public static bool TryGetWindowHandleFromTitle(
            string windowTitle,
            out IntPtr windowHandler,
            int maxRetry = 60,
            int intervalMilliseconds = 1000)
        {
            windowHandler = Retry.Until(
                () => GetWindowHandlesFromTitle(windowTitle).FirstOrDefault(),
                (lw) => !lw.IsDefaultStructValue(),
                maxRetry,
                intervalMilliseconds);
            if (windowHandler.IsDefaultStructValue())
            {
                return false;
            }

            return true;
        }

        public static IEnumerable<WindowInfo> GetWindowInfos(Func<string, bool> titlePredicate)
        {
            var instance = new EnumWindowsFromTitleService(titlePredicate);
            EnumWindows(new EnumWindowsDelegate(instance.EnumWindowCallBack), IntPtr.Zero);
            return instance.windowInfos;
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
}
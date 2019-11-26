using System;
using System.Runtime.InteropServices;

namespace EasySharp.WindowHandles
{
    public static class WindowHandleExtensions
    {
        private static class NativeMethods
        {
            [DllImport("User32.dll")]
            public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetForegroundWindow(IntPtr hWnd);

        }

        public static bool TryGetWindowThreadProcessIdFromWindowHandle(this IntPtr hWnd, out int lpdwProcessId)
        {
            var result = NativeMethods.GetWindowThreadProcessId(hWnd, out lpdwProcessId);
            return result != 0;
        }

        public static bool SetForegroundWindow(this IntPtr hWnd)
        {
            return NativeMethods.SetForegroundWindow(hWnd);
        }
    }
}

using System;
using System.Runtime.InteropServices;

namespace EasySharp.WindowHandles
{
    public static class WindowHandleExtensions
    {
        [DllImport("User32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public static bool TryGetWindowThreadProcessIdFromWindowHandle(this IntPtr hWnd, out int lpdwProcessId)
        {
            var result = GetWindowThreadProcessId(hWnd, out lpdwProcessId);
            return result != 0;
        }

        public static bool SetForeGroundWindow(this IntPtr hWnd)
        {
            return SetForegroundWindow(hWnd);
        }

    }
}

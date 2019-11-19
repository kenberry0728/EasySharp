using System;
using System.Runtime.InteropServices;

namespace EasySharp.WindowHandles
{
    public static class WindowHandleExtensions
    {
        [DllImport("User32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        public static bool TryGetWindowThreadProcessIdFromWindowHandle(this IntPtr hWnd, out int lpdwProcessId)
        {
            var result = GetWindowThreadProcessId(hWnd, out lpdwProcessId);
            return result != 0;
        }
    }
}

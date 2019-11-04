using System;
using System.Runtime.InteropServices;

namespace EasySharpStandard.WindowHandlers
{
    public static class WindowHandlerExtensions
    {
        [DllImport("User32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        public static bool TryGetWindowThreadProcessIdFromWindowHandler(this IntPtr hWnd, out int lpdwProcessId)
        {
            var result = GetWindowThreadProcessId(hWnd, out lpdwProcessId);
            return result != 0;
        }
    }
}

using System.Runtime.InteropServices;

namespace EasySharp.Win.Runtime.InteropServices
{
    public static class Mouse
    {
        private static class NativeMethods
        {
            [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern void SetCursorPos(int X, int Y);

            [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        }

        // https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-mouse_event?redirectedfrom=MSDN
        private enum MouseEvent
        {
            MOUSEEVENTF_ABSOLUTE = 0x8000,
            MOUSEEVENTF_LEFTDOWN = 0x0002,
            MOUSEEVENTF_LEFTUP = 0x0004,
            MOUSEEVENTF_MIDDLEDOWN = 0x0020,
            MOUSEEVENTF_MIDDLEUP = 0x0040,
            MOUSEEVENTF_MOVE = 0x0001,
            MOUSEEVENTF_RIGHTDOWN = 0x0008,
            MOUSEEVENTF_RIGHTUP = 0x0010,
            MOUSEEVENTF_WHEEL = 0x0800, // The wheel has been moved, if the mouse has a wheel. The amount of movement is specified in dwData
                                        // The wheel button is tilted.
            MOUSEEVENTF_XDOWN = 0x0080,
            MOUSEEVENTF_XUP = 0x0100,
            MOUSEEVENTF_HWHEEL = 0x01000,
        }

        public static void LeftClick(int x, int y)
        {
            NativeMethods.SetCursorPos(x, y);
            NativeMethods.mouse_event((int)MouseEvent.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            NativeMethods.mouse_event((int)MouseEvent.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void RightClick(int x, int y)
        {
            NativeMethods.SetCursorPos(x, y);
            NativeMethods.mouse_event((int)MouseEvent.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            NativeMethods.mouse_event((int)MouseEvent.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }
    }
}

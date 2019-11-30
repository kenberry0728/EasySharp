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
            Absolute = 0x8000,
            LEFTDOWN = 0x0002,
            LEFTUP = 0x0004,
            MIDDLEDOWN = 0x0020,
            MIDDLEUP = 0x0040,
            MOVE = 0x0001,
            RIGHTDOWN = 0x0008,
            RIGHTUP = 0x0010,
            WHEEL = 0x0800, // The wheel has been moved, if the mouse has a wheel. The amount of movement is specified in dwData
                            // The wheel button is tilted.
            XDOWN = 0x0080,
            XUP = 0x0100,
            HWHEEL = 0x01000,
        }

        public static void LeftClick(int x, int y)
        {
            NativeMethods.SetCursorPos(x, y);
            NativeMethods.mouse_event((int)MouseEvent.LEFTDOWN, 0, 0, 0, 0);
            NativeMethods.mouse_event((int)MouseEvent.LEFTUP, 0, 0, 0, 0);
        }

        public static void RightClick(int x, int y)
        {
            NativeMethods.SetCursorPos(x, y);
            NativeMethods.mouse_event((int)MouseEvent.RIGHTDOWN, 0, 0, 0, 0);
            NativeMethods.mouse_event((int)MouseEvent.RIGHTUP, 0, 0, 0, 0);
        }
    }
}

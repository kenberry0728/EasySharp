using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace EasySharp.Win.Runtime.InteropServices
{
    public static class Keyboard
    {
        private static class NativeMethods
        {
            [DllImport("user32.dll", EntryPoint = "MapVirtualKeyA")]
            public extern static int MapVirtualKey(int wCode, int wMapType);

            [DllImport("user32.dll")]
            public extern static void SendInput(int nInputs, Input[] pInputs, int cbsize);
        }

        private enum EventType
        {
            INPUT_MOUSE = 0,                 // マウスイベント
            INPUT_KEYBOARD = 1,               // キーボードイベント
            INPUT_HARDWARE = 2,               // ハードウェアイベント
        }

        /// <summary>
        /// シミュレートされたマウスイベントの構造体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "<Pending>")]
        public struct MouseInput
        {
            public int X;
            public int Y;
            public int Data;
            public int Flags;
            public int Time;
            public int ExtraInfo;
        }

        /// <summary>
        /// シミュレートされたキーボードイベントの構造体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "<Pending>")]
        public struct KeyboardInput
        {
            public short VirtualKey;
            public short ScanCode;
            public int Flags;
            public int Time;
            public int ExtraInfo;
        }

        /// <summary>
        /// キーボードやマウス以外の入力デバイスによって生成されたシミュレートされたメッセージの構造体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "<Pending>")]
        public struct HardwareInput
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        /// <summary>
        /// キーストローク、マウスの動き、マウスクリックなどの入力イベントの構造体
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "<Pending>")]
        public struct Input
        {
            [FieldOffset(0)]
            public int Type;

            [FieldOffset(4)]
            public MouseInput Mouse;

            [FieldOffset(4)]
            public KeyboardInput Keyboard;

            [FieldOffset(4)]
            public HardwareInput Hardware;
        }

        /// <summary>
        /// キーボード動作の列挙型
        /// </summary>
        public enum KeyboardStroke
        {
            KEY_DOWN = 0x0000,
            KEY_UP = 0x0002
        }

        /// <summary>
        /// KEYEVENTF_UNICODE
        /// </summary>
        private const int KBD_UNICODE = 0x0004;
            
        public static IEnumerable<Input> CreateKeyboardInputs(string srcStr)
        {
            // TODO: Caps lock / IME
            if (srcStr.IsNullOrEmpty())
            {
                yield break;
            }

            int VK_SHIFT = 0x10;                  // SHIFTキー
            foreach (char s in srcStr)
            {
                var isUpper = char.IsUpper(s);
                if (isUpper)
                {
                    yield return CreateKeyboardInput(KeyboardStroke.KEY_DOWN, (short)VK_SHIFT, (short)s, 0, 0);
                }

                yield return CreateKeyboardInput(KeyboardStroke.KEY_DOWN, 0, (short)s, 0, 0);
                yield return CreateKeyboardInput(KeyboardStroke.KEY_UP, 0, (short)s, 0, 0);

                if (isUpper)
                {
                    yield return CreateKeyboardInput(KeyboardStroke.KEY_UP, (short)VK_SHIFT, (short)s, 0, 0);
                }
            }
        }

        public static Input CreateKeyboardInput(
            KeyboardStroke flags,
            short virtualKey,
            short scanCode,
            int time,
            int extraInfo)
        {
            var input = new Input
            {
                Type = (int)EventType.INPUT_KEYBOARD
            };
            input.Keyboard.Flags = (int)flags | KBD_UNICODE;
            input.Keyboard.VirtualKey = virtualKey;
            input.Keyboard.ScanCode = scanCode;
            input.Keyboard.Time = time;
            input.Keyboard.ExtraInfo = extraInfo;

            return input;
        }

        public static void SendInputs(params Input[] inputs)
        {
            NativeMethods.SendInput(inputs.Length, inputs, Marshal.SizeOf(inputs[0]));
        }
    }
}

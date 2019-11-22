﻿using EasySharp.Logs.Text;
using EasySharp.Threading;
using System.Linq;

namespace EasySharp.Win.Runtime.InteropServices
{
    internal sealed class WindowInfoObserver : PeriodicalObserver<WindowInfo[]>
    {
        public WindowInfoObserver(
            ITextLogger textLogger, 
            int dueTime = 0,
            int periodMilliseconds = 1000) 
            : base(textLogger, dueTime, periodMilliseconds)
        {
        }

        protected override WindowInfo[] Observe()
        {
            return EnumWindowsFromTitleService.GetWindowInfos().ToArray();
        }
    }
}
using EasySharp.Logs.Text;
using EasySharp.Threading;
using System.Linq;

namespace EasySharp.Win.Runtime.InteropServices
{
    public sealed class WindowInfoObserver : PeriodicalObserver<WindowInfo[]>
    {
        public WindowInfoObserver(
            ITextLogger textLogger = null, 
            int dueTime = 0,
            int periodMilliseconds = 1000) 
            : base(textLogger, dueTime, periodMilliseconds)
        {
        }

        protected override WindowInfo[] Observe()
        {
            return EnumWindowsFromTitle.GetWindowInfos().ToArray();
        }

        protected override string GetLoggingText(WindowInfo[] state)
        {
            return state.Select(s => s.ToString()).Join("\t");
        }
    }
}
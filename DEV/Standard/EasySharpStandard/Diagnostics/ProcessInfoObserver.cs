using EasySharp.Logs.Text;
using EasySharp.Threading;
using System.Diagnostics;
using System.Linq;

namespace EasySharp.Diagnostics
{
    public sealed class ProcessInfoObserver : PeriodicalObserver<ProcessInfo[]>
    {
        public ProcessInfoObserver(
            ITextLogger textLogger,
            int dueTime = 0,
            int periodMilliseconds = 1000)
            : base(textLogger, dueTime, periodMilliseconds)
        {
        }

        protected override ProcessInfo[] Observe()
        {
            return Process.GetProcesses().Select(p => new ProcessInfo(p)).ToArray();
        }

        protected override string GetLoggingText(ProcessInfo[] state)
        {
            return state.Select(s => s.ToString()).JoinWithTab();
        }
    }
}
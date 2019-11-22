using EasySharp.Logs.Text;
using EasySharp.Win.WindowHandle;
using EasySharp.Win.WindowHandles;
using System;
using System.Linq;
using System.Threading;

namespace EasySharp.Win.Runtime.InteropServices
{
    internal sealed class WindowInfoObserver : DisposableBase
    {
        private readonly Timer timer;
        private readonly ITextLogger textLogger;

        public WindowInfoObserver(ITextLogger textLogger, int dueTime = 0, int periodMilliseconds = 1000)
        {
            this.textLogger = textLogger;
            this.WindowInfoObservedEvent = new EventContainer<WindowInfo[]>(
                handler => this.WindowInfoObserved += handler,
                handler => this.WindowInfoObserved -= handler);

            this.timer = new Timer(ObserveWindowInfo, null, dueTime, periodMilliseconds);
        }

        public event EventHandler<WindowInfo[]> WindowInfoObserved;

        public IEventContainer<WindowInfo[]> WindowInfoObservedEvent { get; }

        private void ObserveWindowInfo(object state)
        {
            var windowInfos
                = EnumWindowsFromTitleService.GetWindowInfos(s => true)
                  .ToArray();
            this.OnObserved(this, windowInfos);
        }

        public override void DisposeNativeResources()
        {
        }

        public override void DisposeManagedResources()
        {
            this.timer.Dispose();
        }

        private void OnObserved(object sender, WindowInfo[] windowInfos)
        {
            this.textLogger.WriteLine(windowInfos.Select(e => e.ToString()).ToTabSeparated());
            this.WindowInfoObserved?.Invoke(sender, windowInfos);
        }
    }
}
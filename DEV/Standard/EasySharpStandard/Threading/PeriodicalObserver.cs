using EasySharp.Logs.Text;
using System;
using System.Threading;

namespace EasySharp.Win.Runtime.InteropServices
{
    public abstract class PeriodicalObserver<T> : DisposableBase
    {
        private readonly Timer timer;
        private readonly ITextLogger textLogger;

        public PeriodicalObserver(ITextLogger textLogger, int dueTime = 0, int periodMilliseconds = 1000)
        {
            this.textLogger = textLogger;
            this.OveservedEvent = new EventContainer<T>(
                handler => this.Ovserved += handler,
                handler => this.Ovserved -= handler);

            this.timer = new Timer(ObserveWindowInfo, null, dueTime, periodMilliseconds);
        }

        public event EventHandler<T> Ovserved;

        public IEventContainer<T> OveservedEvent { get; }

        private void ObserveWindowInfo(object state)
        {
            this.OnObserved(this, Observe());
        }

        protected abstract T Observe();

        public override void DisposeManagedResources()
        {
            this.timer.Dispose();
        }

        protected virtual string GetLoggingText(T state)
        {
            return state.ToString();
        }

        private void OnObserved(object sender, T state)
        {
            this.textLogger.WriteLine(GetLoggingText(state));
            this.Ovserved?.Invoke(sender, state);
        }
    }
}
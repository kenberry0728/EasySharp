using EasySharp.Logs.Text;
using System;
using System.Threading;

namespace EasySharp.Threading
{
    public abstract class PeriodicalObserver<T> : DisposableBase
    {
        private readonly Timer timer;
        private readonly ITextLogger textLogger;

        protected PeriodicalObserver(
            ITextLogger textLogger,
            int dueTime = 0,
            int periodMilliseconds = 1000)
        {
            this.textLogger = textLogger;
            this.ObeservedEvent = new EventContainer<T>(
                handler => this.Observed += handler,
                handler => this.Observed -= handler);

            this.timer = new Timer(Observe, null, dueTime, periodMilliseconds);
        }

        public event EventHandler<T> Observed;

        public IEventContainer<T> ObeservedEvent { get; }

        private void Observe(object state)
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
            this.Observed?.Invoke(sender, state);
        }
    }
}
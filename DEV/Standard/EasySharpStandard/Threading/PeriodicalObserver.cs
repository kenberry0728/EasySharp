using EasySharp.Logs.Text;
using System;
using System.Threading;

namespace EasySharp.Threading
{
    public abstract class PeriodicalObserver<T> : DisposableBase, IPeriodicalObserver<T>
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
            this.DisposeActions.Add(() =>
            {
                this.timer.Dispose();
            });
        }

        private event EventHandler<T> Observed;

        public IEventContainer<T> ObeservedEvent { get; }

        private void Observe(object timerContext /* null */)
        {
            var state = Observe();
            this.textLogger.WriteLine(GetLoggingText(state));
            this.Observed?.Invoke(this, state);
        }

        protected abstract T Observe();

        protected virtual string GetLoggingText(T state)
        {
            return state.ToString();
        }
    }
}
using EasySharp.Logs.Text;
using System;
using System.Collections.Specialized;
using System.Threading;

namespace EasySharp.Threading
{
    public abstract class PeriodicalObserver<T> : DisposableBase, IPeriodicalObserver<T>
    {
        private Timer timer;
        private readonly ITextLogger textLogger;
        private readonly int dueTime;
        private readonly int periodMilliseconds;

        protected PeriodicalObserver(
            ITextLogger textLogger,
            int dueTime = 0,
            int periodMilliseconds = 1000)
        {
            this.textLogger = textLogger;
            this.dueTime = dueTime;
            this.periodMilliseconds = periodMilliseconds;
            this.ObeservedEvent = new ReferenceCountableEventContainer<T>(
                handler => this.Observed += handler,
                handler => this.Observed -= handler);

            this.timer = CreateTimer();
            this.DisposeActions.Add(() =>
            {
                this?.timer.Dispose();
            });

            this.DisposeActions.Add(
                this.ObeservedEvent.NotifyCollectionChanged.Subscribe(
                OnObserveEventCollectionChanged));
        }

        private Timer CreateTimer()
        {
            return new Timer(Observe, null, this.dueTime, this.periodMilliseconds);
        }

        private void OnObserveEventCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove||
                e.Action == NotifyCollectionChangedAction.Reset)
            {
                if (this.ObeservedEvent.ReferenceCount == 0)
                {
                    this.timer.Dispose();
                    this.timer = null;
                }
            }
            else if (this.timer == null && this.ObeservedEvent.ReferenceCount != 0)
            {
                this.timer = CreateTimer();
            }
        }

        private event EventHandler<T> Observed;

        public IReferenceCountableEventContainer<T> ObeservedEvent { get; }

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
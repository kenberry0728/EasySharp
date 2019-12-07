using EasySharp.Logs.Text;
using System;
using System.Collections;
using System.Linq;

namespace EasySharp.Observer
{
    public abstract class StateObserverBase<TState> : DisposableBase , IStateObserver<TState>
    {
        private readonly ITextLogger textLogger;

        protected StateObserverBase(ITextLogger textLogger = null)
        {
            this.ValueChangeEvent = new EventContainer<ValueChangedEventArg<TState>>(
                handler => this.ValueChange += handler,
                handler => this.ValueChange -= handler);
            this.textLogger = textLogger;
        }

        public event EventHandler<ValueChangedEventArg<TState>> ValueChange;

        public IEventContainer<ValueChangedEventArg<TState>> ValueChangeEvent { get; }

        public TState CurrentValue { get; protected set; }

        protected abstract ValueChangedEventArg<TState> SetCurrentValue(TState value);

        protected virtual void OnValueChange(object sender, ValueChangedEventArg<TState> e)
        {
            e.ThrowArgumentExceptionIfNull(nameof(e));

            if (this.textLogger != null)
            {
                if (e.NewValue is IEnumerable enumerable)
                {
                    var text = enumerable.OfType<object>().Select(s => s.ToString()).JoinWithTab();
                    this.textLogger.WriteLine(this.GetType().Name, text);
                }
                else
                {
                    this.textLogger.WriteLine(this.GetType().Name, e.NewValue.ToString());
                }
            }

            this.ValueChange?.Invoke(sender, e);
        }
    }
}

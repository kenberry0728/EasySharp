using EasySharp.Logs.Text;
using System;
using System.Collections;
using System.Linq;

namespace EasySharp.Observer
{
    public abstract class ValueObserverBase<TState> : DisposableBase , IValueObserver<TState>
    {
        private readonly ITextLogger textLogger;

        protected ValueObserverBase(ITextLogger textLogger = null)
        {
            this.ValueChangeEvent = new EventContainer<ValueChangedEventArg<TState>>(
                handler => this.ValueChange += handler,
                handler => this.ValueChange -= handler);
            this.textLogger = textLogger;
        }

        public event EventHandler<ValueChangedEventArg<TState>> ValueChange;

        public IEventContainer<ValueChangedEventArg<TState>> ValueChangeEvent { get; }

        public TState CurrentValue { get; protected set; }

        protected ValueChangedEventArg<TState> SetCurrentValue(TState value)
        {
            var oldValue = this.CurrentValue;
            this.CurrentValue = value;
            TState newValue = CreateCurrentValueClone();
            return new ValueChangedEventArg<TState>(oldValue, newValue);
        }

        protected abstract TState CreateCurrentValueClone();

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

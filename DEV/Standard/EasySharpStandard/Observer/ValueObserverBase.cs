using EasySharp.Logs.Text;
using System;
using System.Collections;
using System.Linq;

namespace EasySharp.Observer
{
    public abstract class ValueObserverBase<TValue> : DisposableBase , IValueObserver<TValue>
    {
        private readonly ITextLogger textLogger;

        protected ValueObserverBase(ITextLogger textLogger = null)
        {
            this.ValueChangeEvent = new EventContainer<ValueChangedEventArg<TValue>>(
                handler => this.ValueChange += handler,
                handler => this.ValueChange -= handler);
            this.textLogger = textLogger;
        }

        public event EventHandler<ValueChangedEventArg<TValue>> ValueChange;

        public IEventContainer<ValueChangedEventArg<TValue>> ValueChangeEvent { get; }

        public TValue CurrentValue { get; protected set; }

        protected ValueChangedEventArg<TValue> SetCurrentValue(TValue value)
        {
            var oldValue = this.CurrentValue;
            this.CurrentValue = value;
            TValue newValue = CreateCurrentValueClone();
            return new ValueChangedEventArg<TValue>(oldValue, newValue);
        }

        protected abstract TValue CreateCurrentValueClone();

        protected virtual void OnValueChange(object sender, ValueChangedEventArg<TValue> e)
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

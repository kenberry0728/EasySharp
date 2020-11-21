using EasySharp.Logs.Text;
using System;
using System.Collections;
using System.Linq;

namespace EasySharp.Observer
{
    public abstract class ValueObserverBase<TValue> : DisposableBase , IValueObserver<TValue>
    {
        protected ValueObserverBase(ITextLogger textLogger = null)
        {
            this.ValueChangeEvent = new EventContainer<ValueChangedEventArg<TValue>>(
                handler => this.ValueChange += handler,
                handler => this.ValueChange -= handler);
            this.TextLogger = textLogger;
        }

        public event EventHandler<ValueChangedEventArg<TValue>> ValueChange;

        public IEventContainer<ValueChangedEventArg<TValue>> ValueChangeEvent { get; }

        public TValue CurrentValue { get; protected set; }

        protected ITextLogger TextLogger { get; }

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

            if (this.TextLogger != null)
            {
                if (e.NewValue is IEnumerable enumerable)
                {
                    var text = enumerable.OfType<object>().Select(s => s.ToString()).Join("\t");
                    this.TextLogger.WriteLine(this.GetType().Name, text);
                }
                else
                {
                    this.TextLogger.WriteLine(this.GetType().Name, e.NewValue.ToString());
                }
            }

            this.ValueChange?.Invoke(sender, e);
        }
    }
}

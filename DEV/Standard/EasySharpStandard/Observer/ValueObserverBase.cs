using EasySharp.Logs.Text;
using System;

namespace EasySharp.Observer
{
    public abstract class ValueObserverBase<TStateStruct> : IValueObserver<TStateStruct>
        where TStateStruct : struct
    {
        private readonly ITextLogger textLogger;

        protected ValueObserverBase(ITextLogger textLogger = null)
        {
            this.ValueChangeEvent = new EventContainer<ValueChangedEventArg<TStateStruct>>(
                handler => this.ValueChange += handler,
                handler => this.ValueChange -= handler);
            this.textLogger = textLogger;
        }

        public event EventHandler<ValueChangedEventArg<TStateStruct>> ValueChange;

        public IEventContainer<ValueChangedEventArg<TStateStruct>> ValueChangeEvent { get; }

        public TStateStruct CurrentValue { get; private set; }

        public ValueChangedEventArg<TStateStruct> SetCurrentValue(TStateStruct value)
        {
            var oldState = this.CurrentValue;
            this.CurrentValue = value;
            var newState = this.CurrentValue;
            return new ValueChangedEventArg<TStateStruct>(oldState, newState);
        }

        protected virtual void OnValueChange(object sender, ValueChangedEventArg<TStateStruct> e)
        {
            this.textLogger?.WriteLine(this.GetType().Name, e.NewValue.ToString());
            this.ValueChange?.Invoke(sender, e);
        }
    }
}

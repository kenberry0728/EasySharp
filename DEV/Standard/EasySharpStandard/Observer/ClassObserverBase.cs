﻿using EasySharp.Logs.Text;

namespace EasySharp.Observer
{
    public abstract class ClassObserverBase<TValue> : ValueObserverBase<TValue>
        where TValue : class, ICloneable<TValue>
    {
        protected ClassObserverBase(ITextLogger textLogger = null)
            : base(textLogger)
        {
        }

        protected override ValueChangedEventArg<TValue> SetCurrentValue(TValue value)
        {
            var oldValue = this.CurrentValue;
            this.CurrentValue = value;
            var newValue = this.CurrentValue?.Clone();
            return new ValueChangedEventArg<TValue>(oldValue, newValue);
        }
    }
}

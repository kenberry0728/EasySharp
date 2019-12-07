using EasySharp.Logs.Text;

namespace EasySharp.Observer
{
    public abstract class ClassObserverBase<TState> : StateObserverBase<TState>
        where TState : class, ICloneable<TState>
    {
        protected ClassObserverBase(ITextLogger textLogger = null)
            : base(textLogger)
        {
        }

        protected override ValueChangedEventArg<TState> SetCurrentValue(TState value)
        {
            var oldState = this.CurrentValue;
            this.CurrentValue = value;
            var newState = this.CurrentValue?.Clone();
            return new ValueChangedEventArg<TState>(oldState, newState);
        }
    }
}

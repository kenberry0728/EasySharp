using System;

namespace EasySharpStandard.Observers
{
    public abstract class ObserverBase<TStateEnum> : IObserver<TStateEnum> 
        where TStateEnum : struct
    {
        public event EventHandler<StateChangedEventArg<TStateEnum>> StateChange;

        protected void OnStateChange(object sender, StateChangedEventArg<TStateEnum> e)
        {
            this.StateChange?.Invoke(sender, e);
        }
    }
}

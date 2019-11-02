using System;

namespace EasySharpStandard.Observers
{
    public abstract class ObserverBase<TState> : IObserver<TState> 
    {
        public event EventHandler<StateChangedEventArg<TState>> StateChange;

        public TState CurrentState { get; protected set; }

        protected void OnStateChange(object sender, StateChangedEventArg<TState> e)
        {
            this.StateChange?.Invoke(sender, e);
        }
    }
}

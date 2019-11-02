namespace EasySharpStandard.Observers
{
    public class StateChangedEventArg<TState>
    {
        public TState Old { get; protected set; }

        public TState New { get; protected set; }
    }
}

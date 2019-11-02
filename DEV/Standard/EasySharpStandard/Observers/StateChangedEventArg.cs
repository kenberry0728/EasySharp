namespace EasySharpStandard.Observers
{
    public class StateChangedEventArg<TStateEnum>
        where TStateEnum : struct
    {
        public TStateEnum Old { get; protected set; }

        public TStateEnum New { get; protected set; }
    }
}

namespace EasySharp.CQRS
{
    public interface IEventHandler<TEvent>
        where TEvent : IEvent
    {
        bool CanHandle(TEvent @event);
        void Handle(TEvent @event);
    }
}

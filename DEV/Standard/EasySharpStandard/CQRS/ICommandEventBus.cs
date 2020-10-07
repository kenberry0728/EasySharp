namespace EasySharp.CQRS
{
    public interface ICommandEventBus : ICommandBus, IEventBus
    {
    }

    public interface ICommandBus
    {
        void Execute<TCommandType>(TCommandType command) where TCommandType : IIdCommand;
    }

    public interface IEventBus
    {
        void Publish<TEventType>(TEventType @event) where TEventType : IEvent;
        void RegisterEventLister(IIdEventListner eventListner);
        void RegisterGlobalEventLister(IEventListner eventListner);
        void UnregisterEventLister(IIdEventListner idEventListner);
        void UnregisterGlobalEventLister(IEventListner eventListner);
    }
}
namespace EasySharp.CQRS
{
    public interface IIdEventHandler<TIdEvent>
        where TIdEvent : IIdEventType
    {
        bool CanHandle(TIdEvent idEvent);
        void Handle(TIdEvent idEvent);
    }
}

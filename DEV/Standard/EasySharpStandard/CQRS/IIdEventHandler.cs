namespace EasySharp.CQRS
{
    public interface IIdEventHandler<TIdEvent>
        where TIdEvent : IIdEvent
    {
        bool CanHandle(TIdEvent idEvent);
        void Handle(TIdEvent idEvent);
    }
}

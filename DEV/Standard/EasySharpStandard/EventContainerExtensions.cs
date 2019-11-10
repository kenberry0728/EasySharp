using System;

namespace EasySharp
{
    public static class EventContainerExtensions
    {
        public static void DoOrReserve<TEventArg>(
            this IEventContainer<TEventArg> eventContainer,
            Action action,
            Func<bool> needToReserveAction)
        {
            eventContainer.SubscribeEvent(OnReservedEventTriggered);
            if (!needToReserveAction())
            {
                UnsubscribeAndAction();
            }

            void OnReservedEventTriggered(object sender, TEventArg arg)
            {
                UnsubscribeAndAction();
            }

            void UnsubscribeAndAction()
            {
                eventContainer.UnsubscribeEvent(OnReservedEventTriggered);
                action?.Invoke();
            }
        }

        public static void DoAtOnce<TEventArg>(
            this IEventContainer<TEventArg> eventContainer,
            Action action)
        {
            eventContainer.SubscribeEvent(OnTriggered);

            void OnTriggered(object sender, TEventArg arg)
            {
                eventContainer.UnsubscribeEvent(OnTriggered);
                action?.Invoke();
            }
        }
    }
}

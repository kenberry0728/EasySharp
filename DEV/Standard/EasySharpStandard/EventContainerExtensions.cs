using System;

namespace EasySharp
{
    public static class EventContainerExtensions
    {
        public static Action DoOrReserve<TEventArg>(
            this IEventContainer<TEventArg> eventContainer,
            Action action,
            Func<bool> predicate)
        {
            eventContainer.Subscribe(OnReservedEventTriggered);
            if (predicate())
            {
                UnsubscribeAndAction();
            }

            return () => eventContainer.Unsubscribe(OnReservedEventTriggered);

            void OnReservedEventTriggered(object sender, TEventArg arg)
            {
                if (predicate())
                {
                    UnsubscribeAndAction();
                }
            }

            void UnsubscribeAndAction()
            {
                eventContainer.Unsubscribe(OnReservedEventTriggered);
                action();
            }
        }

        public static void DoAtOnce<TEventArg>(
            this IEventContainer<TEventArg> eventContainer,
            Action<object, TEventArg> action)
        {
            eventContainer.DoAtOnce(action, Delegates.True);
        }

        public static void DoAtOnce<TEventArg>(
            this IEventContainer<TEventArg> eventContainer,
            Action<object, TEventArg> action,
            Func<TEventArg, bool> predicate)
        {
            eventContainer.Subscribe(OnTriggered);

            void OnTriggered(object sender, TEventArg arg)
            {
                if(predicate(arg))
                {
                    eventContainer.Unsubscribe(OnTriggered);
                    action(sender, arg);
                }
            }
        }

        public static void Subscribe<TEventArg>(
            this IEventContainer<TEventArg> eventContainer,
            Action<TEventArg> action)
        {
            eventContainer.Subscribe(
                action,
                Delegates.True);
        }
        
        public static void Subscribe<TEventArg>(
            this IEventContainer<TEventArg> eventContainer,
            Action<TEventArg> action,
            Func<TEventArg, bool> predicate)
        {
            eventContainer.Subscribe(OnTriggered);

            void OnTriggered(object sender, TEventArg arg)
            {
                if (predicate(arg))
                {
                    action?.Invoke(arg);
                }
            }
        }

    }
}

using System;

namespace EasySharp
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Extensions")]
    public static class EventContainerExtensions
    {
        public static Action DoOrReserve<TEventArg>(
            this IEventContainer<TEventArg> eventContainer,
            Action action,
            Func<bool> predicate)
        {
            return NewMethod(eventContainer, action, predicate);

        }

        private static Action NewMethod<TEventArg>(IEventContainer<TEventArg> eventContainer, Action action, Func<bool> predicate)
        {
            eventContainer.Subscribe(OnReservedEventTriggered);
            if (predicate())
            {
                UnsubscribeAndAction();
            }

            return () => eventContainer.Unsubscribe(OnReservedEventTriggered);
            #region Local Methods

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

            #endregion
        }

        public static Action DoOrReserve(
            this IEventContainer eventContainer,
            Action action,
            Func<bool> predicate)
        {
            eventContainer.Subscribe(OnReservedEventTriggered);
            if (predicate())
            {
                UnsubscribeAndAction();
            }

            return () => eventContainer.Unsubscribe(OnReservedEventTriggered);

            #region Local Methods

            void OnReservedEventTriggered(object sender, EventArgs arg)
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

            #endregion
        }

        public static Action DoAtOnce<TEventArg>(
            this IEventContainer<TEventArg> eventContainer,
            Action<object, TEventArg> action,
            Func<object, TEventArg, bool> predicate = null)
        {
            predicate = predicate ?? Predicates.True;
            eventContainer.Subscribe(OnTriggered);
            return () => { eventContainer.Unsubscribe(OnTriggered); };

            void OnTriggered(object sender, TEventArg arg)
            {
                if(predicate(sender, arg))
                {
                    eventContainer.Unsubscribe(OnTriggered);
                    action(sender, arg);
                }
            }
        }

        public static Action Subscribe<TEventArg>(
            this IEventContainer<TEventArg> eventContainer,
            Action<object, TEventArg> action,
            Func<object, TEventArg, bool> predicate = null)
        {
            predicate = predicate ?? Predicates.True;
            eventContainer.Subscribe(OnTriggered);
            return () => { eventContainer.Unsubscribe(OnTriggered); };

            void OnTriggered(object sender, TEventArg arg)
            {
                if (predicate(sender, arg))
                {
                    action?.Invoke(sender, arg);
                }
            }
        }

        public static Action PendingSubscribeWhileActing<TEventArg>(
            this IEventContainer<TEventArg> eventContainer,
            Action<object, TEventArg> action,
            Func<object, TEventArg, bool> predicate = null)
        {
            predicate = predicate ?? Predicates.True;
            eventContainer.Subscribe(OnTriggered);

            return () => eventContainer.Unsubscribe(OnTriggered);

            void OnTriggered(object sender, TEventArg arg)
            {
                if (predicate(sender, arg))
                {
                    eventContainer.Unsubscribe(OnTriggered);
                    action(sender, arg);
                    eventContainer.Subscribe(OnTriggered);
                }
            }
        }
    }
}

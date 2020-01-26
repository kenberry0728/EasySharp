using EasySharp.Collections.Generic;
using System;
using System.Collections.Generic;

namespace EasySharp.CQRS
{
    public class CommandEventBus : ICommandEventBus
    {
        private readonly Dictionary<Guid, ICommandSaga> idToCommandSagas
            = new Dictionary<Guid, ICommandSaga>();

        private readonly Dictionary<Guid, List<IIdEventListner>> idToIdEventListeners
            = new Dictionary<Guid, List<IIdEventListner>>();

        private readonly List<IEventListner> globalEventListeners
            = new List<IEventListner>();

        public void Execute<TCommandType>(TCommandType command)
            where TCommandType : ICommand
        {
            var sagaStarter = command as ISagaStartCommand;
            if (sagaStarter != null)
            {
                var saga = sagaStarter.Create(sagaStarter.Id);
                this.idToCommandSagas.Add(saga.Id, saga);
                var commandHandler = saga as ICommandHandler<TCommandType>;
                commandHandler.Execute(command);
                return;
            }

            if (this.idToCommandSagas is IIdCommand idCommand)
            {
                if (this.idToCommandSagas.TryGetValue(idCommand.Id, out var runningSaga))
                {
                    if (runningSaga is ICommandHandler<TCommandType> commandHandler
                        && commandHandler.CanExecute(command))
                    {
                        commandHandler.Execute(command);
                    }

                    if (runningSaga.IsComplete)
                    {
                        this.idToCommandSagas.Remove(idCommand.Id);
                    }
                }
            }
        }

        public void RegisterGlobalEventLister(IEventListner eventListner)
        {
            this.globalEventListeners.Add(eventListner);
        }

        public void UnregisterGlobalEventLister(IEventListner eventListner)
        {
            this.globalEventListeners.Remove(eventListner);
        }

        public void RegisterEventLister(IIdEventListner eventListner)
        {
            this.idToIdEventListeners.Add(eventListner.Id, eventListner);
        }

        public void UnregisterEventLister(IIdEventListner idEventListner)
        {
            this.idToIdEventListeners.Remove(idEventListner.Id, idEventListner);
        }

        public void Publish<TEventType>(TEventType @event)
            where TEventType : IEvent
        {
            //foreach (var saga in this.idToCommandSagas)
            //{
            //    Handle(saga.Value, @event);
            //}

            foreach (var globalEventLister in this.globalEventListeners)
            {
                Handle(globalEventLister, @event);
            }

            if (@event is IIdEventType idEvent)
            {
                if (this.idToIdEventListeners.TryGetValue(idEvent.Id, out var idEventListeners))
                {
                    foreach (var idEventListner in idEventListeners)
                    {
                        Handle(idEventListner, idEvent);
                    }
                }

                if (this.idToCommandSagas.TryGetValue(idEvent.Id, out var runningSaga))
                {
                    Handle(runningSaga, idEvent);
                    if (runningSaga.IsComplete)
                    {
                        this.idToCommandSagas.Remove(idEvent.Id);
                    }
                }
            }
        }

        private static void Handle(object idEventHandler, IIdEventType idEvent)
        {
            if (idEventHandler is IIdEventHandler<IIdEventType> eventHandler
                && eventHandler.CanHandle(idEvent))
            {
                eventHandler.Handle(idEvent);
            }
        }

        private static void Handle<TEventType>(IEventListner handler, TEventType @event)
            where TEventType : IEvent
        {
            if (handler is IEventHandler<TEventType> eventHandler
                && eventHandler.CanHandle(@event))
            {
                eventHandler.Handle(@event);
            }
        }
    }
}

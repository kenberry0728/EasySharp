using EasySharp.Collections.Generic;
using System;
using System.Collections.Generic;

namespace EasySharp.CQRS
{
    public class CommandEventBus : ICommandEventBus
    {
        #region Fields
        private readonly Dictionary<Guid, ICommandSaga> idToCommandSagas
            = new Dictionary<Guid, ICommandSaga>();

        private readonly Dictionary<Guid, List<IIdEventListner>> idToIdEventListeners
            = new Dictionary<Guid, List<IIdEventListner>>();

        private readonly List<IEventListner> globalEventListeners
            = new List<IEventListner>();

        #endregion

        #region Public Methods

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

        public void Execute<TCommandType>(TCommandType command)
            where TCommandType : IIdCommand
        {
            var sagaStarter = command as ISagaStartCommand;
            if (sagaStarter != null)
            {
                var saga = sagaStarter.Create(this);
                this.idToCommandSagas.Add(saga.Id, saga);
                var commandHandler = saga as IIdCommandHandler<TCommandType>;
                commandHandler.Execute(command);
            }
            else if (command is IIdCommand idCommand)
            {
                if (this.idToCommandSagas.TryGetValue(idCommand.Id, out var runningSaga))
                {
                    if (runningSaga is IIdCommandHandler<TCommandType> commandHandler
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

            if (@event is IIdEvent idEvent)
            {
                if (this.idToIdEventListeners.TryGetValue(idEvent.Id, out var idEventListeners))
                {
                    foreach (var idEventListner in idEventListeners)
                    {
                        HandleIdEvent(idEventListner, idEvent);
                    }
                }

                if (this.idToCommandSagas.TryGetValue(idEvent.Id, out var runningSaga))
                {
                    HandleIdEvent(runningSaga, idEvent);
                    if (runningSaga.IsComplete)
                    {
                        this.idToCommandSagas.Remove(idEvent.Id);
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        private static void HandleIdEvent(object idEventHandler, IIdEvent idEvent)
        {
            if (idEventHandler is IIdEventHandler<IIdEvent> eventHandler
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

        #endregion
    }
}

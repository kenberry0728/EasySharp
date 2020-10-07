using EasySharp.CQRS.Sample;
using EasySharp.CQRS.Sample.Application;
using EasySharp.CQRS.Sample.Domain;
using System;

namespace EasySharp.CQRS.Sampel
{
    public class AandBSaga : ISagaStartCommand
                            , ICommandSaga
                            , IIdCommandHandler<DoAandBCommand>
                            , IIdEventHandler<ACompletedEvent>
                            , IIdEventHandler<ACanceledEvent>
                            , IIdEventHandler<BCompletedEvent>
                            , IIdEventHandler<BCanceledEvent>

    {
        private readonly ICommandEventBus commandEventBus;

        private AandBSaga(ICommandEventBus commandEventBus)
        {
            this.commandEventBus = commandEventBus;
        }

        public ICommandSaga Create(ICommandEventBus commandEventBus)
        {
            return new AandBSaga(commandEventBus);
        }

        public bool IsComplete { get; private set; }

        public Guid Id { get; private set; }

        public bool CanExecute(DoAandBCommand message)
        {
            return message.Id == this.Id;
        }

        public void Execute(DoAandBCommand message)
        {
            this.Id = message.Id;
            var aCommand = new ACommand(this.Id);
            this.commandEventBus.Execute(aCommand);
        }

        public bool CanHandle(ACompletedEvent @event)
        {
            return @event.Id == this.Id;
        }

        public bool CanHandle(ACanceledEvent @event)
        {
            return @event.Id == this.Id;
        }

        public void Handle(ACompletedEvent message)
        {
            var bCommand = new BCommand(this.Id);
            this.commandEventBus.Execute(bCommand);
        }

        public void Handle(ACanceledEvent message)
        {
            // なんかEventいる？
            this.IsComplete = true;
        }

        public bool CanHandle(BCompletedEvent idEvent)
        {
            return this.Id == idEvent.Id;
        }

        public void Handle(BCompletedEvent message)
        {
            this.IsComplete = true;
        }

        public bool CanHandle(BCanceledEvent idEvent)
        {
            return this.Id == idEvent.Id;
        }

        public void Handle(BCanceledEvent message)
        {
            this.IsComplete = true;
        }
    }
}

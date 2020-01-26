using EasySharp.CQRS.Sample;
using EasySharp.CQRS.Sample.Application;
using EasySharp.CQRS.Sample.Domain;
using System;

namespace EasySharp.CQRS.Sampel
{
    public class AandBSaga : CQRS.ICommandSaga
                            , ICommandHandler<DoAandBCommand>
                            , IEventHandler<ACompletedEvent>
                            , IEventHandler<ACanceledEvent>
                            , IEventHandler<BCompletedEvent>
                            , IEventHandler<BCanceledEvent>

    {
        private readonly ICommandEventBus commandEventBus;

        public AandBSaga(ICommandEventBus commandEventBus)
        {
            this.commandEventBus = commandEventBus;
        }

        public bool IsComplete { get; private set; }

        public Guid Id { get; } = Guid.NewGuid();

        public bool CanExecute(DoAandBCommand message)
        {
            return message.Id == this.Id;
        }

        public void Execute(DoAandBCommand message)
        {
            var aCommand = new ACommand(this.Id);
            this.commandEventBus.Execute(aCommand);
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

        public void Handle(BCompletedEvent message)
        {
            this.IsComplete = true;
        }

        public void Handle(BCanceledEvent message)
        {
            this.IsComplete = true;
        }
    }
}

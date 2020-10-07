namespace EasySharp.CQRS
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        bool CanExecute(TCommand message);

        void Execute(TCommand message);
    }
}

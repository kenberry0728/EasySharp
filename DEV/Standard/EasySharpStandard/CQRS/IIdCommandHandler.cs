namespace EasySharp.CQRS
{
    public interface IIdCommandHandler<TIdCommand>
        where TIdCommand : IIdCommand
    {
        bool CanExecute(TIdCommand message);

        void Execute(TIdCommand message);
    }
}

namespace EasySharp.DesignPatterns.StrategyPattern
{
    public interface IStrategy
    {
        void Execute();
    }

    public interface IStrategy<TParameter>
    {
        void Execute(TParameter parameter);
    }

    public interface IStrategy<TParameter, TResult>
    {
        TResult Execute(TParameter parameter);
    }
}
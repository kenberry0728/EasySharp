namespace EasySharp.DesignPatterns.FactoryPattern
{
    public interface IFactory<TInstance>
    {
        TInstance Create();
    }

    public interface IFactory<TArgument, TInstance>
    {
        TInstance Create(TArgument arg);
    }

    public interface IFactory<TArgument1, TArgument2, TInstance>
    {
        TInstance Create(TArgument1 arg1, TArgument2 arg2);
    }

    public interface IFactory<TArgument1, TArgument2, TArgument3, TInstance>
    {
        TInstance Create(TArgument1 arg1, TArgument2 arg2, TArgument3 arg3);
    }
}
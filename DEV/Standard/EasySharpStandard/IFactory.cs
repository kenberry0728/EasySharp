namespace EasySharp
{
    public interface IFactory<T>
    {
        T Create();
    }

    public interface IFactory<TArg, TInst>
    {
        TInst Create(TArg arg);
    }

    public interface IFactory<TArg1, TArg2, TInst>
    {
        TInst Create(TArg1 arg1, TArg2 arg2);
    }

    public interface IFactory<TArg1, TArg2, TArg3, TInst>
    {
        TInst Create(TArg1 arg1, TArg2 arg2, TArg3 arg3);
    }
}
namespace EasySharp
{
    public interface IInitialize<TArg, TInst> where TInst : new()
    {
        TInst Initialize(TArg arg);
    }

    public interface IInitialize<TArg1, TArg2, TInst> where TInst : new()
    {
        TInst Initialize(TArg1 arg1, TArg2 arg2);
    }

    public interface ICreateWithInitialize<TArg1, TArg2, TArg3, TInst> where TInst : new()
    {
        TInst Initialize(TArg1 arg1, TArg2 arg2, TArg3 arg3);
    }
}
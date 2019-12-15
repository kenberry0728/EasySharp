namespace EasySharp
{
    public class Factory<T> : IFactory<T> where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }

    public class Factory<TArg, TInst> : IFactory<TArg, TInst>
        where TInst : IInitialize<TArg, TInst>, new()
    {
        public TInst Create(TArg arg)
        {
            var newInstance = new TInst();
            newInstance.Initialize(arg);
            return newInstance;
        }
    }

    public class Factory<TArg1, TArg2,TInst> : IFactory<TArg1, TArg2, TInst>
        where TInst : IInitialize<TArg1, TArg2, TInst>, new()
    {
        public TInst Create(TArg1 arg1, TArg2 arg2)
        {
            var newInstance = new TInst();
            newInstance.Initialize(arg1, arg2);
            return newInstance;
        }
    }

    public class Factory<TArg1, TArg2, TArg3, TInst> : IFactory<TArg1, TArg2, TArg3, TInst>
    where TInst : ICreateWithInitialize<TArg1, TArg2, TArg3, TInst>, new()
    {
        public TInst Create(TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var newInstance = new TInst();
            newInstance.Initialize(arg1, arg2, arg3);
            return newInstance;
        }
    }
}

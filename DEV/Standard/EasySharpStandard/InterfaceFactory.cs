namespace EasySharp
{
    public class InterfaceFactory<TInst, TInstInterfarce> : IFactory<TInstInterfarce>
        where TInst : TInstInterfarce, new()
    {
        public TInstInterfarce Create()
        {
            return new TInst();
        }
    }
    public class InterfaceFactory<TArg, TInst, TInstInterface> : IFactory<TArg, TInstInterface>
        where TInst : IInitialize<TArg, TInst>, TInstInterface, new()
    {
        public TInstInterface Create(TArg arg)
        {
            var newInstance = new TInst();
            newInstance.Initialize(arg);
            return newInstance;
        }
    }

    public class InterfaceFactory<TArg1, TArg2, TInst, TInstInterface> : IFactory<TArg1, TArg2, TInstInterface>
        where TInst : IInitialize<TArg1, TArg2, TInst>, TInstInterface, new()
    {
        public TInstInterface Create(TArg1 arg1, TArg2 arg2)
        {
            var newInstance = new TInst();
            newInstance.Initialize(arg1, arg2);
            return newInstance;
        }
    }

    public class InterfaceFactory<TArg1, TArg2, TArg3, TInst, TInstInterface> : IFactory<TArg1, TArg2, TArg3, TInstInterface>
    where TInst : ICreateWithInitialize<TArg1, TArg2, TArg3, TInst>, TInstInterface, new()
    {
        public TInstInterface Create(TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var newInstance = new TInst();
            newInstance.Initialize(arg1, arg2, arg3);
            return newInstance;
        }
    }
}

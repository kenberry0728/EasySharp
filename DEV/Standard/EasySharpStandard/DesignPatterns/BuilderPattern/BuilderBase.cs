namespace EasySharp.DesignPatterns.BuilderPattern
{
    public abstract class BuilderBase<TInstance>
    where TInstance : new()
    {
        protected BuilderBase()
        {
            this.Instance = new TInstance();
        }

        protected TInstance Instance { get; }

        public abstract BuilderBase<TInstance> Customize();

        public TInstance Build()
        {
            this.Customize();
            return this.Instance;
        }
    }
}
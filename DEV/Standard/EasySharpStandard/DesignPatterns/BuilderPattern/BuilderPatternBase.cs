namespace EasySharp.DesignPatterns.BuilderPattern
{
    public abstract class BuilderPatternBase<TInstance>
    where TInstance : new()
    {
        private readonly TInstance instance;

        protected BuilderPatternBase()
        {
            this.instance = new TInstance();
        }

        public abstract BuilderPatternBase<TInstance> BuildUpInstance(TInstance instance);

        public TInstance Build()
        {
            this.BuildUpInstance(this.instance);
            return this.instance;
        }
    }
}
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

        public TInstance Build()
        {
            this.BuildUp(this.instance);
            return this.instance;
        }

        protected abstract BuilderPatternBase<TInstance> BuildUp(TInstance instance);
    }
}
namespace EasySharp.DesignPatterns.DecoratorPattern
{
    public class DecoratorBase<TComponent>
    {
        public DecoratorBase(TComponent component)
        {
            this.Component = component;
        }

        protected TComponent Component { get; }
    }

    #region Sample

    interface IDecoratorSampleComponent
    {
        void DoSomething();
    }

    class ExistedComponent : IDecoratorSampleComponent
    {
        public void DoSomething()
        {
        }
    }

    class DecoratorSampleComponents 
        : DecoratorBase<IDecoratorSampleComponent>, IDecoratorSampleComponent
    {
        public DecoratorSampleComponents(IDecoratorSampleComponent component) 
            : base(component)
        {
        }

        public void DoSomething()
        {
            this.PreDoSomething();
            this.Component.DoSomething();
            this.PostDoSomething();
        }

        private void PreDoSomething()
        {
        }

        private void PostDoSomething()
        {
        }
    }

    class DecoratorComponentConsumer
    {
        private readonly IDecoratorSampleComponent component;

        public DecoratorComponentConsumer(IDecoratorSampleComponent component)
        {
            this.component = component;
        }

        public DecoratorComponentConsumer() 
        // : this(new ExistingComponent())
        :this(new DecoratorSampleComponents(new ExistedComponent()))
        {
        }

        public void Run()
        {
            this.component.DoSomething();
        }
    }

    #endregion
}
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

    #region Keep existing behaviour region

    interface IComponent
    {
        void DoSomething();
    }

    class ExistingComponent : IComponent
    {
        public void DoSomething() { }
    }

    #endregion

    class AddingBehaviourComponent 
        : DecoratorBase<IComponent>, IComponent
    {
        public AddingBehaviourComponent(IComponent component) 
            : base(component)
        { }

        public void DoSomething()
        {
            this.PreDoSomething();
            this.Component.DoSomething();
            this.PostDoSomething();
        }

        private void PreDoSomething() { }
        private void PostDoSomething() { }
    }

    class ComponentFactory
    {
        public IComponent Create()
        {
            // Adding behaviour Point.

            // From
            // return new ExistingComponent();

            // To
            return new AddingBehaviourComponent(new ExistingComponent());
        }
    }

    #endregion
}
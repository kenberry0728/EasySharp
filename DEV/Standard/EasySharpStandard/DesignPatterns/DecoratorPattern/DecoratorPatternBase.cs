﻿namespace EasySharp.DesignPatterns.DecoratorPattern
{
    public class DecoratorPatternBase<TComponent>
    {
        public DecoratorPatternBase(TComponent component)
        {
            this.Component = component;
        }

        protected TComponent Component { get; }
    }

    class Sample
    {
        #region Keep existing behaviour region

        interface IComponent
        {
            void DoSomething();
        }

        class ExistingComponent : IComponent
        {
            public void DoSomething()
            {
            }
        }

        #endregion

        class AddingBehaviourComponent
            : DecoratorPatternBase<IComponent>, IComponent
        {
            public AddingBehaviourComponent(IComponent component)
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
    }
}
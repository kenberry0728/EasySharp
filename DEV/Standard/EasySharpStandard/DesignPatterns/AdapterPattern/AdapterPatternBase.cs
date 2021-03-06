﻿namespace EasySharp.DesignPatterns.AdapterPattern
{
    public class AdapterPatternBase<TComponent>
    {
        public AdapterPatternBase(TComponent component)
        {
            this.Component = component;
        }

        protected TComponent Component { get; }
    }

    class Sample
    {
        #region Keep existing behaviour region

        class ExistingComponent
        {
            public void DoSomething() { }
        }

        #endregion

        interface IExpectingInterface
        {
        }

        class ExpectingInterfacedClass
            : AdapterPatternBase<ExistingComponent>, IExpectingInterface
        {
            public ExpectingInterfacedClass(ExistingComponent component)
                : base(component)
            { }
        }

        class ComponentFactory
        {
            // From
            //public ExistingComponent Create()
            //{
            //    return new ExistingComponent();
            //}

            // Using as 1st party Point.
            // To
            public IExpectingInterface Create()
            {
                return new ExpectingInterfacedClass(new ExistingComponent());
            }
        }
    }
}
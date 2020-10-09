﻿using System.Collections.Generic;

namespace EasySharp.DesignPatterns.CompositePattern
{
    public class CompositeBase<TComponent> 
        : List<TComponent>
    {
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
    
    class ExistingComponentComponentUser
    {
        private readonly IComponent component;

        public ExistingComponentComponentUser(IComponent component)
        {
            this.component = component;
        }

        public void Run()
        {
            this.component.DoSomething();
        }
    }

    #endregion

    class AddingBehaviourComponent : IComponent
    {
        public void DoSomething() { }
    }

    class CompositeComponents
        : CompositeBase<IComponent>, IComponent
    {
        public void DoSomething()
        {
            this.ForEach(c => c.DoSomething());
        }
    }

    class ComponentFactory
    {
        public ExistingComponentComponentUser Create()
        {
            // Adding behaviour Point.

            // From
            // return new ExistingComponentComponentUser(new ExistingComponent());

            // To
            return new ExistingComponentComponentUser(
                new CompositeComponents
                {
                    new ExistingComponent(), 
                    new AddingBehaviourComponent()
                });
        }
    }

    #endregion
}
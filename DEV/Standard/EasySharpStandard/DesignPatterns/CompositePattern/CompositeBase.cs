using System.Collections.Generic;

namespace EasySharp.DesignPatterns.CompositePattern
{
    public class CompositeBase<TComponent> 
        : List<TComponent>
    {
    }

    #region Sample

    interface ISampleComponent
    {
        void DoSomething();
    }

    class ExistedComponent : ISampleComponent
    {
        public void DoSomething()
        {
        }
    }

    class AddingBehaviourComponent : ISampleComponent
    {
        public void DoSomething()
        {
        }
    }

    class SampleCompositeComponents 
        : CompositeBase<ISampleComponent>, ISampleComponent
    {
        public void DoSomething()
        {
            this.ForEach(c => c.DoSomething());
        }
    }

    class CompositeConsumer
    {
        private readonly ISampleComponent component;

        public CompositeConsumer(ISampleComponent component)
        {
            this.component = component;
        }

        public CompositeConsumer() 
            //: this(new ExistedComponent())
        : this(new SampleCompositeComponents { new ExistedComponent(), new AddingBehaviourComponent()})
        {
        }

        public void Run()
        {
            this.component.DoSomething();
        }
    }

    class CompositeConsumerFactory
    {
    }

    #endregion
}
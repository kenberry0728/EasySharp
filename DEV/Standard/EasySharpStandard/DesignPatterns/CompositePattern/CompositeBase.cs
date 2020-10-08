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

    class SampleComponentConsumer
    {
        private readonly ISampleComponent component;

        public SampleComponentConsumer(ISampleComponent component)
        {
            this.component = component;
        }

        public void Run()
        {
            this.component.DoSomething();
        }
    }

    class SampleCompositeConsumerFactory
    {
        public SampleComponentConsumer Create()
        {
            // From
            // return new SampleComponentConsumer(new ExistedComponent());

            // To
            return new SampleComponentConsumer(
                new SampleCompositeComponents
                {
                    new ExistedComponent(), 
                    new AddingBehaviourComponent()
                });
        }
    }

    #endregion
}
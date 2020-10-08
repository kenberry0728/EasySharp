using System.Collections.Generic;

namespace EasySharp.DesignPatterns.CompositePattern
{
    public class CompositeBase<TComponent> 
        : List<TComponent>
    {
    }

    #region Sample
#if EnableSample
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

    class AddedBehaviourComponent : ISampleComponent
    {
        public void DoSomething()
        {
        }
    }

    class SampleComponents 
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
        : this(new SampleComponents { new ExistedComponent(), new AddedBehaviourComponent()})
        {
        }

        public void Run()
        {
            this.component.DoSomething();
        }
    }
#endif
    #endregion
}
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.CQRS.Sample
{
    class Main
    {
        public void Run()
        {
            var commandEventBus = new CommandEventBus();
            commandEventBus.RegisterSagaFactory(new DoAandBSagaFactory());
        }
    }
}

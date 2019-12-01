using EasySharp;
using EasySharp.Observer;
using EasySharp.ProgressNotifications.Core;
using EasySharp.ProgressNotifications.Core.Models;
using EasySharpStandardConsole.ProgressNotifications.Core;
using EasySharpStandardConsoleTest.ConsoleTests.Implementations;
using System;
using System.Diagnostics;

namespace EasySharpStandardConsoleTest.ProgressNotifications
{
    internal class EventContainerTests : ConsoleTestBase
    {
        private class EventClass : ValueObserverBase<bool>
        {
            public void SetAndRaiseEvent(bool value)
            {
                var arg = this.SetCurrentValue(value);
                this.OnValueChange(this, arg);
            }
        }

        public override void Run()
        {
            var eventClass = new EventClass();

            var unsubAction1 = eventClass.ValueChangeEvent.Subscribe(action1, Predicates.True);
            var unsubAction2 = eventClass.ValueChangeEvent.Subscribe(action2, Predicates.True);

            Console.WriteLine("Subscribe");

            eventClass.SetAndRaiseEvent(true);
            eventClass.SetAndRaiseEvent(false);

            void action1(object sender, ValueChangedEventArg<bool> e)
            {
                Console.WriteLine(nameof(action1));
            }

            void action2(object sender, ValueChangedEventArg<bool> e)
            {
                Console.WriteLine(nameof(action2));
            }

            Console.WriteLine("Unsubscribe1");
            unsubAction1();
            eventClass.SetAndRaiseEvent(true);
            eventClass.SetAndRaiseEvent(false);

            Console.WriteLine("Unsubscribe2");
            unsubAction2();
            eventClass.SetAndRaiseEvent(true);
            eventClass.SetAndRaiseEvent(false);

            var output = this.ReadToEnd();
            Debug.Assert(
@"Subscribe
action1
action2
action1
action2
Unsubscribe1
action2
action2
Unsubscribe2
"==  output);
        }
    }
}

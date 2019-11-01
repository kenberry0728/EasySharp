using System.Windows.Automation;

namespace EasySharpCoreWpfLibrary.UIAutomations.Core
{
    public static class ConditionFactory
    {
        public static Condition EditControlProperty() 
            => new PropertyCondition(
                AutomationElement.ControlTypeProperty,
                ControlType.Edit);

        public static Condition NameProperty(string name)
            => new PropertyCondition(
                AutomationElement.NameProperty,
                name);

    }
}

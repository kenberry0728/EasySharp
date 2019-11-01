using System.Windows.Automation;

namespace EasySharpCoreWpfLibrary.UIAutomations.Core
{
    public static class ConditionFactory
    {
        public static Condition EditControlPropertyCondition() 
            => new PropertyCondition(
                AutomationElement.ControlTypeProperty,
                ControlType.Edit);

        public static Condition NamePropertyCondition(string name)
            => new PropertyCondition(
                AutomationElement.NameProperty,
                name);

    }
}

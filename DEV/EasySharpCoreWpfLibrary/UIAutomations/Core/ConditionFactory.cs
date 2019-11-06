using System.Windows.Automation;

namespace EasySharpCoreWpfLibrary.UIAutomations.Core
{
    public static class ConditionFactory
    {
        public static Condition EditControlProperty() 
            => new PropertyCondition(
                AutomationElement.ControlTypeProperty,
                ControlType.Edit);

        public static Condition ButtonControlProperty()
            => new PropertyCondition(
                AutomationElement.ControlTypeProperty,
                ControlType.Button);

        public static Condition PaneControlProperty()
            => new PropertyCondition(
                AutomationElement.ControlTypeProperty,
                ControlType.Pane);

        public static Condition NameProperty(string name)
            => new PropertyCondition(
                AutomationElement.NameProperty,
                name);

    }
}

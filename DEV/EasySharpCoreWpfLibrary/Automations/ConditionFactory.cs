using System.Windows.Automation;

namespace EasySharpCoreWpfLibrary.Automations
{
    public static class ConditionFactory
    {
        public static Condition NameProperty(string name)
            => new PropertyCondition(
                AutomationElement.NameProperty,
                name);

        public static Condition AutomationIdProperty(string automationId)
            => new PropertyCondition(
                AutomationElement.AutomationIdProperty,
                automationId);
    }
}

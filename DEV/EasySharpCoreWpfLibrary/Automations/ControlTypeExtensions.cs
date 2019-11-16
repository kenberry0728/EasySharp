using System.Windows.Automation;

namespace EasySharpCoreWpfLibrary.Automations
{
    public static class ControlTypeExtensions
    {
        public static Condition ToCondition(this ControlType controlType)
        {
            return new PropertyCondition(AutomationElement.ControlTypeProperty,
                controlType);
        }
    }
}

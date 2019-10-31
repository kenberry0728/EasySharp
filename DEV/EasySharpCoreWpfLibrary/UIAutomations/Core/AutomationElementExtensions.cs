using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;

namespace EasySharpStandard.UIAutomations.Core
{
    public static class AutomationElementExtensions
    {
        public static IEnumerable<AutomationElement> FindAll(
            this AutomationElement automationElement,
            TreeScope treeScope,
            params Condition[] conditions)
        {
            var elements = automationElement.FindAll(treeScope, conditions[0]).OfType<AutomationElement>();
            for (int i = 1; i < conditions.Length; i++)
            {
                elements = elements.OfType<AutomationElement>().Where(ae => ae.FindFirst(TreeScope.Element, conditions[i]) != null);
            }

            return elements;
        }

        public static bool Invoke(
            this AutomationElement automationElement,
            string value)
        {
            if (automationElement.GetCurrentPattern(ValuePattern.Pattern) is InvokePattern buttonPattern)
            {
                buttonPattern.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool SetValue(
            this AutomationElement automationElement,
            string value)
        {
            if (automationElement.GetCurrentPattern(ValuePattern.Pattern) is ValuePattern valuePattern)
            {
                valuePattern.SetValue(value);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetValue(this AutomationElement automationElement)
        {
            if (automationElement.GetCurrentPattern(ValuePattern.Pattern) is ValuePattern valuePattern
                && !valuePattern.Current.IsReadOnly)
            {
                return valuePattern.Current.Value;
            }

            return null;
        }
    }
}

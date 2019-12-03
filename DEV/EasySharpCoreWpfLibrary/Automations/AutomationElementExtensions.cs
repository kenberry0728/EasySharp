using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;

namespace EasySharpCoreWpfLibrary.Automations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Extensions")]
    public static class AutomationElementExtensions
    {
        public static IEnumerable<AutomationElement> FindAll(
            this AutomationElement automationElement,
            TreeScope treeScope,
            params Condition[] conditions)
        {
            return automationElement
                .FindAll(treeScope, new AndCondition(conditions))
                .OfType<AutomationElement>();
        }

        public static bool SetValueToEditControlWithin(
            this AutomationElement rootElement,
            string setValue,
            params Condition[] conditions)
        {
            var elements = rootElement.FindAll(
                TreeScope.Descendants,
                conditions.Concat(new[] { ControlType.Edit.ToCondition() }).ToArray())
                .ToList();
            if (elements.Count != 1)
            {
                return false;
            }

            elements[0].SetValue(setValue);
            return true;
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

        public static bool PressButtonControlWithin(
            this AutomationElement rootElement,
            params Condition[] conditions)
        {
            var elements = rootElement.FindAll(
                TreeScope.Descendants,
                conditions.Concat(new[] { ControlType.Button.ToCondition() }).ToArray())
                .ToList();
            if (elements.Count != 1)
            {
                return false;
            }

            return elements[0].Invoke();
        }

        public static bool Invoke(this AutomationElement automationElement)
        {
            // todo: press = invoke?
            if (automationElement.GetCurrentPattern(InvokePattern.Pattern) is InvokePattern butotn)
            {
                butotn.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Contains(
            this AutomationElement rootElement,
            params Condition[] conditions)
        {
            var condition = conditions.Length == 1 
                ? conditions[0] 
                : new AndCondition(conditions);
            return rootElement.FindFirst(
                TreeScope.Descendants,
                condition) != null;
        }

        public static bool ContainsChild(
            this AutomationElement rootElement,
            params Condition[] conditions)
        {
            return GetFirstChild(rootElement, conditions) != null;
        }

        public static AutomationElement GetFirstChild(
            this AutomationElement parent,
            params Condition[] conditions)
        {
            var condition = conditions.Length == 1
                ? conditions[0]
                : new AndCondition(conditions);
            return parent.FindFirst(
                TreeScope.Children,
                condition);
        }
    }
}

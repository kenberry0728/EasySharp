using EasySharpStandard.Rails.Attributes;
using System.Reflection;

namespace EasySharpWpf.Views.Rails.Core
{
    public static class MemberInfoExtensions
    {
        public static bool HasVisibleRailsBindAttribute(this MemberInfo element)
        {
            return element.GetCustomAttribute<RailsBindAttribute>()?.UserVisible == true;
        }
    }
}

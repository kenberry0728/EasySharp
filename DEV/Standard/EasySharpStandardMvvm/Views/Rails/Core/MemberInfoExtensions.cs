using System.Reflection;
using EasySharpStandardMvvm.Attributes.Rails;

namespace EasySharpStandardMvvm.Views.Rails.Core
{
    public static class MemberInfoExtensions
    {
        public static bool HasVisibleRailsBindAttribute(this MemberInfo element)
        {
            return element.GetCustomAttribute<RailsBindAttribute>()?.UserVisible == true;
        }

        public static bool HasRailsSourceBindAttribute(this MemberInfo element)
        {
            return element.GetCustomAttribute<RailsSourceBindAttribute>() != null;
        }

    }
}

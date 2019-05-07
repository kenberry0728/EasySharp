using System.ComponentModel;
using System.Reflection;

namespace EasySharpStandard.Attributes.Core
{
    public static class DisplayNameAttributeExtensions
    {
        public static string GetDisplayName(this MemberInfo memberInfo)
        {
            var displayNameAttribute
                = memberInfo.GetCustomAttribute<DisplayNameAttribute>();
            return displayNameAttribute?.DisplayName ?? memberInfo.Name;
        }
    }
}

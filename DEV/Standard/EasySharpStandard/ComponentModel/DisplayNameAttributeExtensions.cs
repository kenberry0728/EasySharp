using System.ComponentModel;
using System.Reflection;

namespace EasySharp.ComponentModel
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

using System.ComponentModel;
using System.Reflection;

namespace EasySharp.ComponentModel.Reflection
{
    public static class DisplayNameAttributeExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Extensions")]
        public static string GetDisplayName(this MemberInfo memberInfo)
        {
            var displayNameAttribute
                = memberInfo.GetCustomAttribute<DisplayNameAttribute>();
            return displayNameAttribute?.DisplayName ?? memberInfo.Name;
        }
    }
}

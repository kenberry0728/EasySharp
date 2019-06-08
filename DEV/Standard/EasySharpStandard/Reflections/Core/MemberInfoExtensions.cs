using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace EasySharpStandard.Reflections.Core
{
    public static class MemberInfoExtensions
    {
        public static string GetRelativeMemberPath(this MemberInfo memberInfo)
        {
            var assemblyName = memberInfo.DeclaringType.Assembly.GetName().Name;
            var relativeNamespacePath = memberInfo.DeclaringType.FullName;
            if (relativeNamespacePath.StartsWith(assemblyName))
            {
                relativeNamespacePath = relativeNamespacePath.Substring(assemblyName.Length + 1);
            }
            else
            {
                Debug.Assert(false, "namespace should follow the folder structure.");
            }

            var folderPath = string.Join(
                @"\",
                relativeNamespacePath.Split('.'));
            return Path.Combine(folderPath, memberInfo.Name);
        }
    }
}

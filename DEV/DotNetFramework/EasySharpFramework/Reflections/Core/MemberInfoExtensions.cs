using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace EasySharpStandard.Reflections.Core
{
    public static class MemberInfoExtensions
    {
        public static string GetRelativeTypePath(this Type type)
        {
            var assemblyName = type.Assembly.GetName().Name;
            var relativeNamespacePath = type.FullName;
            if (relativeNamespacePath != null && relativeNamespacePath.StartsWith(assemblyName))
            {
                relativeNamespacePath = relativeNamespacePath.Substring(assemblyName.Length + 1);
            }
            else
            {
                Debug.Assert(false, "namespace should follow the folder structure.");
            }

            return string.Join(
                @"\",
                relativeNamespacePath.Split('.'));
        }

        public static string GetRelativeMemberPath(this MemberInfo memberInfo)
        {
            var assemblyName = memberInfo.DeclaringType.Assembly.GetName().Name;
            var relativeNamespacePath = memberInfo.DeclaringType.FullName;
            if (relativeNamespacePath != null && relativeNamespacePath.StartsWith(assemblyName))
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

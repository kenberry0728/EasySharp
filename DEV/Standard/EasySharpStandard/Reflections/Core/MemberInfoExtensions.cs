using System.IO;
using System.Reflection;

namespace EasySharpStandard.Reflections.Core
{
    public static class MemberInfoExtensions
    {
        public static string GetRelativePropertyPath(this MemberInfo property)
        {
            var assemblyName = property.DeclaringType.Assembly.GetName().Name;
            var relativeNamespacePath = property.DeclaringType.FullName;
            if (relativeNamespacePath.StartsWith(assemblyName))
            {
                relativeNamespacePath = relativeNamespacePath.Substring(assemblyName.Length + 1);
            }

            var folderpath = string.Join(
                @"\",
                relativeNamespacePath.Split('.'));
            return Path.Combine(folderpath, property.Name);
        }
    }
}

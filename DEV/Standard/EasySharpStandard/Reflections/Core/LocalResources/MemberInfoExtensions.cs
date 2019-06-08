using EasySharpStandard.DiskIO.Extensions;
using EasySharpStandard.SafeCodes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasySharpStandard.Reflections.Core.LocalResources
{
    public static class MemberInfoExtensions
    {
        public static IEnumerable<string> GetLocalResourceValues(this Type type, string propertyName)
        {
            return type.GetProperty(propertyName).GetLocalResourceValues();
        }

        public static IEnumerable<string> GetLocalResourceValues(this MemberInfo memberInfo)
        {
            return GetSelectableItems(memberInfo.GetRelativeMemberPath());
        }

        private static IEnumerable<string> GetSelectableItems(string filePath)
        {
            Try.To(() =>
                {
                    var content = filePath.ReadToEnd();
                    return content.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                        .Distinct();
                },
                out var selectableItems);

            return selectableItems ?? new string[0];
        }
    }
}

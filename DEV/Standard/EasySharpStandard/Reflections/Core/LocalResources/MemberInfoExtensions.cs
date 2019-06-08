using EasySharpStandard.DiskIO.Extensions;
using EasySharpStandard.SafeCodes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasySharpStandard.Collections.Dictionaries.Core;
using EasySharpStandard.DataStructures.Trees.Core.Models;

namespace EasySharpStandard.Reflections.Core.LocalResources
{
    public static class MemberInfoExtensions
    {
        #region GetLocalResourceDependentValues

        public static IDictionary<string, List<string>> GetLocalResourceDependentValues(this Type type, string propertyName)
        {
            return type.GetProperty(propertyName).GetLocalResourceDependentValues();
        }

        public static IDictionary<string, List<string>> GetLocalResourceDependentValues(this MemberInfo memberInfo)
        {
            return GetSelectableDependentItems(memberInfo.GetRelativeMemberPath());
        }

        private static IDictionary<string, List<string>> GetSelectableDependentItems(string filePath)
        {
            var content = filePath.ReadToEnd();
            var indentedStrings = 
                content.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => new IndentedString(s));
            var currentKey = string.Empty;
            var dictionary = new Dictionary<string, List<string>>();
            foreach (var indentedString in indentedStrings)
            {
                if (indentedString.Depth == 0)
                {
                    currentKey = indentedString.Content;
                }
                else
                {
                    dictionary.Add(currentKey, indentedString.Content);
                }
            }

            return dictionary;
        }

        #endregion

        #region GetLocalResourceValues

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
                    return content.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                },
                out var selectableItems);

            return selectableItems ?? new string[0];
        }

        #endregion
    }
}

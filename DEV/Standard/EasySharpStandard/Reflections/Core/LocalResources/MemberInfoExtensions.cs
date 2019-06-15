using EasySharpStandard.Collections.Dictionaries.Core;
using EasySharpStandard.DataStructures.Trees.Core.Models;
using EasySharpStandard.SafeCodes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasySharpStandard.DiskIO.Serializers;

namespace EasySharpStandard.Reflections.Core.LocalResources
{
    public static class MemberInfoExtensions
    {
        #region GetLocalResourceDependentValues

        public static IDictionary<string, List<string>> GetLocalResourceDependentValues(
            this Type type, 
            string propertyName,
            string fileExtension = "")
        {
            return type.GetProperty(propertyName).GetLocalResourceDependentValues(fileExtension);
        }

        public static IDictionary<string, List<string>> GetLocalResourceDependentValues(
            this MemberInfo memberInfo,
            string fileExtension = "")
        {
            return GetSelectableDependentItems(memberInfo.GetRelativeMemberPath() + fileExtension);
        }

        #endregion

        #region GetLocalResourceValues

        public static IEnumerable<string> GetLocalResourceValues(this Type type, string propertyName, string fileExtension = "")
        {
            return type.GetProperty(propertyName).GetLocalResourceValues(fileExtension);
        }

        public static IEnumerable<string> GetLocalResourceValues(this MemberInfo memberInfo, string fileExtension = "")
        {
            return GetSelectableItems(memberInfo.GetRelativeMemberPath() + fileExtension);
        }

        private static IEnumerable<string> GetSelectableItems(string filePath)
        {
            Try.To(() =>
                {
                    var content = filePath.ReadToEnd();
                    return content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                },
                out var selectableItems);

            return selectableItems ?? new string[0];
        }

        private static IDictionary<string, List<string>> GetSelectableDependentItems(string filePath)
        {
            Try.To(() =>
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
            }, out var result);

            return result;
        }

        #endregion
    }
}

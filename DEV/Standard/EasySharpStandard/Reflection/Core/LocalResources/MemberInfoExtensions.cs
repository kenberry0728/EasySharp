using EasySharp.DataStructures.Trees.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasySharp.Collections.Generic;
using EasySharp.IO;

namespace EasySharp.Reflection.Core.LocalResources
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
            return GetDependentItems(memberInfo.GetRelativeMemberPath() + fileExtension);
        }

        public static IDictionary<string, List<string[]>> GetLocalResourceDependentMultiValues(
            this Type type,
            string propertyName,
            string fileExtension = "")
        {
            return type.GetLocalResourceDependentValues(propertyName, fileExtension)
                .ToDependentMultiValues();
        }

        public static IDictionary<string, List<string[]>> GetLocalResourceDependentMultiValues(
            this MemberInfo memberInfo,
            string fileExtension = "")
        {
            return memberInfo.GetLocalResourceDependentValues(fileExtension)
                .ToDependentMultiValues();
        }

        #endregion

        #region GetLocalResourceValues

        public static IEnumerable<string> GetLocalResourceValues(this Type type, string propertyName, string fileExtension = "")
        {
            return type.GetProperty(propertyName).GetLocalResourceValues(fileExtension);
        }

        public static IEnumerable<string[]> GetLocalResourceMultiValues(this Type type, string propertyName, string fileExtension = "")
        {
            return type.GetProperty(propertyName).GetLocalResourceValues(fileExtension).Select(v => v.ToMultiValues());
        }

        public static IEnumerable<string> GetLocalResourceValues(this MemberInfo memberInfo, string fileExtension = "")
        {
            return GetSelectableItems(memberInfo.GetRelativeMemberPath() + fileExtension);
        }

        public static IEnumerable<string[]> GetLocalResourceUIValues(this MemberInfo memberInfo, string fileExtension = "")
        {
            return GetSelectableItems(memberInfo.GetRelativeMemberPath() + fileExtension).Select(v => v.ToMultiValues());
        }

        #endregion

        #region Private Methods

        private static IDictionary<string, List<string[]>> ToDependentMultiValues(this IDictionary<string, List<string>> localResourceDependentValues)
        {
            return localResourceDependentValues.ToDictionary(d => d.Key, d => d.Value.Select(s => s.ToMultiValues()).ToList());
        }

        private static string[] ToMultiValues(this string s)
        {
            return s.Split('\t');
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

        private static IDictionary<string, List<string>> GetDependentItems(string filePath)
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

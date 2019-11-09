using EasySharp.Reflections.Core;
using EasySharpStandardMvvm.Attributes.Rails;
using System;

namespace EasySharpStandardMvvm.Models.Rails.Core
{
    public static class RailsObjectExtensions
    {
        public static void CopyRailsBindPropertyValues(this object from, object to, Type type)
        {
            type.CopyPropertyValues(
                from,
                to,
                p => p.HasCustomAttribute<RailsDataMemberBindAttribute>()
                     && p.CanRead
                     && p.CanWrite);
        }
    }
}

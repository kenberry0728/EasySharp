using EasySharpStandard.Reflections.Core;
using System;

namespace EasySharpStandardMvvm.Rails.Attributes
{
    public static class RailsObjectExtensions
    {
        public static void CopyRailsBindPropertyValues(this object from, object to, Type type)
        {
            type.CopyPropertyValues(
                from,
                to,
                p => p.HasCustomAttribute<RailsBindAttribute>()
                     && p.CanRead
                     && p.CanWrite);
        }
    }
}

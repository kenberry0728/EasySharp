using System;
using System.ComponentModel;

namespace EasySharpStandard.Attributes.Core
{
    [AttributeUsage(
        AttributeTargets.Enum |
        AttributeTargets.Field)]
    public class EnumDisplayNameAttribute : DisplayNameAttribute
    {
        public EnumDisplayNameAttribute(string displayName)
            :base(displayName)
        {
        }
    }
}

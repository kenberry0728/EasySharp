using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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

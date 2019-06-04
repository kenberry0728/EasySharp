﻿using EasySharpStandard.Attributes.Core;
using EasySharpStandardMvvm.Attributes.Rails;
using System.Reflection;

namespace EasySharpStandardMvvm.Models.Rails.Core
{
    public class RailsModel
    {
        public override string ToString()
        {
            return this.ToCommaSeparatedString(
                p => p.GetCustomAttribute<RailsBindAttribute>()?.UserVisible == true);
        }
    }
}

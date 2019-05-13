﻿using EasySharpStandard.Attributes.Core;
using EasySharpWpf.ViewModels.Rails.Attributes;
using System.Reflection;

namespace EasySharpWpf.Models.Core
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
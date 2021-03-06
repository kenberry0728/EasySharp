﻿using System.Collections.Generic;
using System.Linq;

namespace EasySharpStandardMvvm.Models.Rails.Core
{
    public class RailsCollection<T> : List<T>
        where T : class
    {
        public override string ToString()
        {
            return string.Join(",", this.Select(i => i.ToString()));
        }
    }
}

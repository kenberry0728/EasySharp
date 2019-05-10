﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EasySharpStandard.Collections.Core
{
    public static class IListExtensions
    {
        public static IEnumerable<object> ToEnumerable(this IList list)
        {
            return list.OfType<object>();
        }
    }
}

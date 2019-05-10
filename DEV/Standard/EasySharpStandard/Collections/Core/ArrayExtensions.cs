﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EasySharpStandard.Collections.Core
{
    public static class ArrayExtensions
    {
        public static IEnumerable<object> ToEnumerable(this Array array)
        {
            return array.OfType<object>();
        }
    }
}
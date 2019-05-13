using System.Collections.Generic;
using System.Linq;

namespace EasySharpWpf.Models.Core
{
    public class RailsList<T> : List<T>
        where T : class
    {
        public override string ToString()
        {
            return string.Join(",", this.Select(i => i.ToString()));
        }
    }
}

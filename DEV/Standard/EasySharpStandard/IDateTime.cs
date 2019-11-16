using System;

namespace EasySharp
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime Today { get; }
        DateTime UtcNow { get; }
    }
}

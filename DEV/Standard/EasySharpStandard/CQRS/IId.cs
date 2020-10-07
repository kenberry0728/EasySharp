using System;

namespace EasySharp.CQRS
{
    public interface IId
    {
        Guid Id { get; }
    }
}

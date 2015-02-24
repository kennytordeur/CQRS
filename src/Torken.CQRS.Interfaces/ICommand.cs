using System;

namespace Torken.CQRS.Interfaces
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}
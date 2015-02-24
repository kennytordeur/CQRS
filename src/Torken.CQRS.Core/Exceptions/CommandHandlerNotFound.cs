using System;

namespace Torken.CQRS.Core.Exceptions
{
    [Serializable]
    public class CommandHandlerNotFound : Exception
    {
    }
}
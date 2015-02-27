namespace Torken.CQRS.Core
{
    using System;
    using Torken.CQRS.Interfaces;

    public class CommandDispatcher : AbstractCommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override ICommandHandler<T> ResolveCommandHandler<T>()
        {
            return _serviceProvider.GetService(typeof(ICommandHandler<T>)) as ICommandHandler<T>;
        }

        protected override ICommandValidator<T> ResolveCommandValidator<T>()
        {
            return _serviceProvider.GetService(typeof(ICommandValidator<T>)) as ICommandValidator<T>;
        }
    }
}
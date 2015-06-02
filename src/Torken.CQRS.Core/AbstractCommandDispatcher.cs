namespace Torken.CQRS.Core
{
    using Torken.CQRS.Core.Exceptions;
    using Torken.CQRS.Interfaces;

    public abstract class AbstractCommandDispatcher : ICommandDispatcher
    {
        protected abstract ICommandHandler<T> ResolveCommandHandler<T>() where T : ICommand;

        protected abstract ICommandValidator<T> ResolveCommandValidator<T>() where T : ICommand;

        public virtual ICommandResult Send<T>(T command) where T : ICommand
        {
            var commandResult = new CommandResult();
            var commandValidator = ResolveCommandValidator<T>();

            if (null != commandValidator)
                commandResult.AddCommandResult(commandValidator.Validate(command));

            if (commandResult.IsSuccess)
            {
                var handler = ResolveCommandHandler<T>();

                if (null == handler)
                {
                    throw new CommandHandlerNotFound();
                }

                commandResult.AddCommandResult(handler.Execute(command));
            }

            return commandResult;
        }

        public virtual ICommandResult SendAsync<T>(T command) where T : ICommand
        {
            throw new System.NotImplementedException();
        }
    }
}
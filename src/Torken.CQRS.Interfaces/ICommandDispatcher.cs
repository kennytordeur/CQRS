namespace Torken.CQRS.Interfaces
{
    public interface ICommandDispatcher
    {
        ICommandResult Send<T>(T command) where T : ICommand;

        ICommandResult SendAsync<T>(T command) where T : ICommand;
    }
}
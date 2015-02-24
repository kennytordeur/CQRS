namespace Torken.CQRS.Interfaces
{
    public interface ICommandHandler<T> where T : ICommand
    {
        ICommandResult Execute(T command);
    }
}
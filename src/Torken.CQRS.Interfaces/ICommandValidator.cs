namespace Torken.CQRS.Interfaces
{
    public interface ICommandValidator<T> where T : ICommand
    {
        ICommandResult Validate(T command);
    }
}
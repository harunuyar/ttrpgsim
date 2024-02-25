namespace Dnd.System.Entities;

using Dnd.System.CommandSystem.Commands;

public interface ICommandHandler
{
    Task HandleCommand(ICommand command);
}

namespace Dnd.System.Entities;

using Dnd.System.CommandSystem.Commands;

public interface IBonusProvider
{
    Task HandleCommand(ICommand command);
}

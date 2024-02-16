namespace Dnd.System.Entities;

using Dnd.System.CommandSystem.Commands.BaseCommands;

public interface IBonusProvider : IDndEntity
{
    void HandleCommand(ICommand command);
}

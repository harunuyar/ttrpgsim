namespace Dnd.System.Entities;

using Dnd.System.CommandSystem.Commands;

public interface IBonusProvider : IDndEntity
{
    void HandleCommand(DndCommand command);
}

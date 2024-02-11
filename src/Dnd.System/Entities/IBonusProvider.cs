namespace Dnd.Entities;

using Dnd.CommandSystem.Commands;

public interface IBonusProvider : IDndEntity
{
    void HandleCommand(DndCommand command);
}

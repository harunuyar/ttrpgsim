namespace DnD.Entities;

using DnD.CommandSystem.Commands;

internal interface IBonusProvider : IDndEntity
{
    void HandleCommand(DndCommand command);
}

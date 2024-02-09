namespace DnD.Entities;

using DnD.CommandSystem.Commands;

internal interface IBonusProvider
{
    public string Name { get; }

    void HandleCommand(DndCommand command);
}

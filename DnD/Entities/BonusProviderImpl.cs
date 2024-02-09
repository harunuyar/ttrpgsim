namespace DnD.Entities;

using DnD.CommandSystem.Commands;

internal class BonusProviderImpl : IBonusProvider
{
    public BonusProviderImpl(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public void HandleCommand(DndCommand command) { }
}

namespace Dnd.Entities.Traits;

using Dnd.CommandSystem.Commands;

public abstract class ATrait : IBonusProvider
{
    public ATrait(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; }

    public string Description { get; }

    public virtual void HandleCommand(DndCommand command)
    {
    }
}
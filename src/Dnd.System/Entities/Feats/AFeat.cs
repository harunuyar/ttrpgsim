namespace Dnd.Entities.Feats;

using Dnd.CommandSystem.Commands;

public abstract class AFeat : IBonusProvider
{
    public AFeat(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name {  get; }

    public string Description { get; }

    public virtual void HandleCommand(DndCommand command)
    {
    }
}

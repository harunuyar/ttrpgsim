namespace Dnd.Entities.Effects;

using Dnd.CommandSystem.Commands;

public abstract class AEffect : IBonusProvider
{
    public AEffect(string name, string description)
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

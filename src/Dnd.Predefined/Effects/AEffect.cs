namespace Dnd.Predefined.Effects;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effects;

public abstract class AEffect : IEffect
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

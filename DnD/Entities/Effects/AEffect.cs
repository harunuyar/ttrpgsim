namespace DnD.Entities.Effects;

using DnD.CommandSystem.Commands;

internal abstract class AEffect : IBonusProvider
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

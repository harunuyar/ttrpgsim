namespace Dnd.System.Entities.Effects;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Effects.Duration;

public class AEffect : IEffect
{
    public AEffect(string name, string description, IEffectDuration duration)
    {
        Name = name;
        Description = description;
        Duration = duration;
    }

    public string Name { get; }

    public string Description { get; }

    public IEffectDuration Duration { get; }

    public virtual void HandleCommand(DndCommand command)
    {
        Duration.HandleCommand(command);
        if (Duration.IsExpired())
        {
            command.Character.Effects.Remove(this);
        }
    }
}

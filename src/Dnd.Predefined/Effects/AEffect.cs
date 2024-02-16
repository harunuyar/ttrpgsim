namespace Dnd.Predefined.Effects;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects;
using Dnd.System.Entities.Effects.Duration;
using Dnd.System.CommandSystem.Commands.BaseCommands;

public class AEffect : IEffect
{
    public AEffect(string name, string description, IEffectDuration duration, IGameActor source, IGameActor target)
    {
        Name = name;
        Description = description;
        Duration = duration;
        Source = source;
        Target = target;
    }

    public string Name { get; }

    public string Description { get; }

    public IEffectDuration Duration { get; }

    public IGameActor Source { get; }

    public IGameActor Target { get; }

    public virtual void HandleCommand(ICommand command)
    {
    }
}

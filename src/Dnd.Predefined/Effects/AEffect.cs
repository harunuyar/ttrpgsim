namespace Dnd.Predefined.Effects;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects;
using Dnd.System.Entities.Effects.Duration;

public class AEffect : IEffect
{
    public AEffect(string name, string description, IEffectDuration duration, ICharacter source, ICharacter target)
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

    public ICharacter Source { get; }

    public ICharacter Target { get; }

    public virtual void HandleCommand(ICommand command)
    {
    }
}

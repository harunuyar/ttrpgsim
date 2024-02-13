namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;

public class Restrained : AEffect
{
    public Restrained(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Restrained", "A restrained creature's speed becomes 0, and it can't benefit from any bonus to its speed.", duration, source, target)
    {
    }
}

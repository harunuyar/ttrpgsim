namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;

public class Grappled : AEffect
{
    public Grappled(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Grappled", "A grappled creature's speed becomes 0, and it can't benefit from any bonus to its speed.", duration, source, target)
    {
    }
}

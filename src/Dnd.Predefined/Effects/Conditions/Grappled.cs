namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.Effects;
using Dnd.System.Entities.Effects.Duration;

public class Grappled : AEffect
{
    public Grappled(IEffectDuration duration)
        : base("Grappled", "A grappled creature's speed becomes 0, and it can't benefit from any bonus to its speed.", duration)
    {
    }
}

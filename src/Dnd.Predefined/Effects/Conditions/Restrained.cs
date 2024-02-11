namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.Effects;
using Dnd.System.Entities.Effects.Duration;

public class Restrained : AEffect
{
    public Restrained(IEffectDuration duration)
        : base("Restrained", "A restrained creature's speed becomes 0, and it can't benefit from any bonus to its speed.", duration)
    {
    }
}

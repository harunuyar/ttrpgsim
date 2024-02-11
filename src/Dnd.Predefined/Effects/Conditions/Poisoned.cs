namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.Effects;
using Dnd.System.Entities.Effects.Duration;

public class Poisoned : AEffect
{
    public Poisoned(IEffectDuration duration)
        : base("Poisoned", "A poisoned creature has disadvantage on attack rolls and ability checks.", duration)
    {
    }
}

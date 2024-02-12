namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects.Duration;

public class Poisoned : AEffect
{
    public Poisoned(IEffectDuration duration, ICharacter source, ICharacter target)
        : base("Poisoned", "A poisoned creature has disadvantage on attack rolls and ability checks.", duration, source, target)
    {
    }
}

namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects.Duration;

public class Unconscious : AEffect
{
    public Unconscious(IEffectDuration duration, ICharacter source, ICharacter target)
        : base("Unconscious", "An unconscious creature is incapacitated, can't move or speak, and is unaware of its surroundings.", duration, source, target)
    {
    }
}

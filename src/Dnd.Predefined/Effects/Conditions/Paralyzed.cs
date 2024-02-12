namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects.Duration;

public class Paralyzed : AEffect
{
    public Paralyzed(IEffectDuration duration, ICharacter source, ICharacter target)
        : base("Paralyzed", "A paralyzed creature is incapacitated and can't move or speak. The creature automatically fails Strength and Dexterity saving throws. Attack rolls against the creature have advantage. Any attack that hits the creature is a critical hit if the attacker is within 5 feet of the creature.", 
            duration, source, target)
    {
    }
}

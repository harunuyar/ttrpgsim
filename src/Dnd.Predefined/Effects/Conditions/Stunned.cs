namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects.Duration;

public class Stunned : AEffect
{
    public Stunned(IEffectDuration duration, ICharacter source, ICharacter target)
        : base("Stunned", "A stunned creature is incapacitated, can't move, and can speak only falteringly.", duration, source, target)
    {
    }
}

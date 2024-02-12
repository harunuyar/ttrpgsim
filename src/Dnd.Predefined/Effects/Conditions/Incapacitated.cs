namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Effects.Duration;

public class Incapacitated : AEffect
{
    public Incapacitated(IEffectDuration duration, ICharacter source, ICharacter target)
        : base("Incapacitated", "An incapacitated creature can't take actions or reactions.", duration, source, target)
    {
    }
}

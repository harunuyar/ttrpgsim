namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.Effects;
using Dnd.System.Entities.Effects.Duration;

public class Incapacitated : AEffect
{
    public Incapacitated(IEffectDuration duration)
        : base("Incapacitated", "An incapacitated creature can't take actions or reactions.", duration)
    {
    }
}

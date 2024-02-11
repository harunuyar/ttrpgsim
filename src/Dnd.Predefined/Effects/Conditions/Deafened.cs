namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.Effects;
using Dnd.System.Entities.Effects.Duration;

public class Deafened : AEffect
{
    public Deafened(IEffectDuration duration) 
        : base("Deafened", "A deafened creature can't hear and automatically fails any ability check that requires hearing.", duration)
    {
    }
}

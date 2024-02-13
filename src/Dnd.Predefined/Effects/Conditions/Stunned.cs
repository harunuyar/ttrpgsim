namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;

public class Stunned : AEffect
{
    public Stunned(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Stunned", "A stunned creature is incapacitated, can't move, and can speak only falteringly.", duration, source, target)
    {
    }
}

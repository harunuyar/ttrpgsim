namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;

public class Incapacitated : AEffect
{
    public Incapacitated(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Incapacitated", "An incapacitated creature can't take actions or reactions.", duration, source, target)
    {
    }
}

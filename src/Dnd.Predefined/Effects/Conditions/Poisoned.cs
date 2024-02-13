namespace Dnd.Predefined.Effects.Conditions;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Effects.Duration;

public class Poisoned : AEffect
{
    public Poisoned(IEffectDuration duration, IGameActor source, IGameActor target)
        : base("Poisoned", "A poisoned creature has disadvantage on attack rolls and ability checks.", duration, source, target)
    {
    }
}

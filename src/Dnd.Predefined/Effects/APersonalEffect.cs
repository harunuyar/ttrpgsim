namespace Dnd.Predefined.Effects;

using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public abstract class APersonalEffect : AEffect, IPersonalEffect
{
    public APersonalEffect(string name, string desc, EffectDuration duration, IGameActor source, IGameActor target)
        : base(name, desc, duration, source)
    {
        Target = target;
    }

    public IGameActor Target { get; }
}

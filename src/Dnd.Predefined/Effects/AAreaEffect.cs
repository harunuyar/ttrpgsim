namespace Dnd.Predefined.Effects;

using Dnd._5eSRD.Models.Common;
using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public abstract class AAreaEffect : AEffect, IAreaEffect
{
    public AAreaEffect(string name, string desc, AreaOfEffectModel areaOfEffect, EffectDuration duration, IGameActor source, IEnumerable<IGameActor> actorsInArea)
        : base(name, desc, duration, source)
    {
        AreaOfEffect = areaOfEffect;
        GameActorsInArea = new HashSet<IGameActor>(actorsInArea);
    }

    public AreaOfEffectModel AreaOfEffect { get; }

    public HashSet<IGameActor> GameActorsInArea { get; }
}

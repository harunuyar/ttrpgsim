namespace Dnd.System.Entities.Effect;

using Dnd._5eSRD.Models.Common;
using Dnd.System.Entities.GameActor;

public interface IAreaEffect : IEffect
{
    AreaOfEffectModel AreaOfEffect { get; }
    HashSet<IGameActor> GameActorsInArea { get; }
}

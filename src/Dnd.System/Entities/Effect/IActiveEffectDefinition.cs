namespace Dnd.System.Entities.Effect;

using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public interface IActiveEffectDefinition : IEffectDefinition
{
    EEffectActivationTime ActivationTime { get; }
    IEffectEvent CreateEvent(IGameActor source, IGameActor target);
}

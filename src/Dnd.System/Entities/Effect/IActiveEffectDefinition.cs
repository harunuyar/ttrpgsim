namespace Dnd.System.Entities.Effect;

using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public interface IActiveEffectDefinition : IEffectDefinition
{
    Task<bool> ShouldActivate(IGameActor source, IGameActor target, IEvent eventToReactTo);
    Task<IEvent> CreateEvent(IGameActor source, IGameActor target, IEvent eventToReactTo);
}

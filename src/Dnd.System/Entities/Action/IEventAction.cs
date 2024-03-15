namespace Dnd.System.Entities.Action;

using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public interface IEventAction : IAction
{
    Task<IEvent> CreateEvent(IGameActor gameActor);
}

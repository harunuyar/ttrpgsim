namespace Dnd.Predefined.Actions;

using Dnd.System.Entities.Action;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public abstract class EventAction : Action, IEventAction
{
    public EventAction(string name, ActionDurationType actionDurationType, IEnumerable<IActionUsageLimit> usageLimits)
        : base(name, actionDurationType, usageLimits)
    {
    }

    public abstract Task<IEvent> CreateEvent(IGameActor actor);
}

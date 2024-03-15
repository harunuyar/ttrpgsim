namespace Dnd.Predefined.Actions;

using Dnd.System.Entities.Action;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public abstract class EventReaction : Action, IEventReaction
{
    protected EventReaction(string name, ActionDurationType actionDurationType, IEnumerable<IActionUsageLimit> usageLimits, bool mandatory) 
        : base(name, actionDurationType, usageLimits)
    {
        MandatoryReaction = mandatory;
    }

    public bool MandatoryReaction { get; }

    public abstract Task<IEvent> CreateReactionEvent(IGameActor actor, IEvent eventToReactTo);

    public abstract Task<bool> IsReactionAvailable(IGameActor gameActor, IEvent eventToReactTo);
}

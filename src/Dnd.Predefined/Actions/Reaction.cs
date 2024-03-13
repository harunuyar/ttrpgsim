namespace Dnd.Predefined.Actions;

using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public abstract class Reaction : Action, IReaction
{
    public Reaction(string name, ActionDurationType actionDurationType, IEnumerable<IActionUsageLimit> usageLimits, bool isMandatory) : base(name, actionDurationType, usageLimits)
    {
        MandatoryReaction = isMandatory;
    }

    public bool MandatoryReaction { get; }

    public override Task<IEvent> CreateEvent(IGameActor actor)
    {
        throw new InvalidOperationException("Reactions do not create their own events");
    }

    public abstract Task<IEvent> CreateReactionEvent(IGameActor actor, IEvent eventToReactTo);

    public abstract Task<bool> IsReactionAvailable(IGameActor gameActor, IEvent eventToReactTo);
}

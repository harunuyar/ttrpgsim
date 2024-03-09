namespace Dnd.Predefined.Actions;

using Dnd.Predefined.Commands.BoolCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public abstract class Reaction : Action, IReaction
{
    public Reaction(string name, ActionDurationType actionDurationType, IEnumerable<IActionUsageLimit> usageLimits, EReactionType reactionType, bool isMandatory) : base(name, actionDurationType, usageLimits)
    {
        ReactionType = reactionType;
        MandatoryReaction = isMandatory;
    }

    public EReactionType ReactionType { get; }

    public bool MandatoryReaction { get; }

    public bool CanReactTo(EReactionType reactionType)
    {
        return (ReactionType & reactionType) == ReactionType;
    }

    public override IActionEvent CreateEvent(IGameActor actor)
    {
        throw new InvalidOperationException("Reactions do not create their own events");
    }

    public abstract IActionEvent CreateReactionEvent(IGameActor actor, IActionEvent eventToReactTo);

    public virtual Task<bool> IsReactionAvailable(IGameActor gameActor, IActionEvent eventToReactTo, EReactionType reactionType)
    {
        return Task.FromResult(CanReactTo(reactionType));
    }
}

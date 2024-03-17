namespace Dnd.Predefined.Definitions.Actions.Fighter;

using Dnd.Predefined.Actions;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class IndomitableAction : EventReaction
{
    public IndomitableAction(ActionUsageLimit usageLimit)
        : base("Indomitable: Reroll Saving Throw", ActionDurationType.FreeAction, [usageLimit], false)
    {
    }

    public override Task<bool> IsReactionAvailable(IGameActor gameActor, IEvent eventToReactTo)
    {
        return Task.FromResult(gameActor == eventToReactTo.EventOwner
            && eventToReactTo is ISuccessRollEvent successRollEvent
            && successRollEvent.SuccessRollAction.SuccessRollType == ESuccessRollType.Save
            && successRollEvent.RollResult.HasValue
            && successRollEvent.RollResult.Value.IsFailure());
    }

    public override Task<IEvent> CreateReactionEvent(IGameActor actor, IEvent eventToReactTo)
    {
        if (eventToReactTo is not ISuccessRollEvent successRollEvent || successRollEvent.SuccessRollAction.SuccessRollType != ESuccessRollType.Save)
        {
            throw new InvalidOperationException("IndomitableAction can only react to saving throw events");
        }

        if (successRollEvent.RawRollResult is null || successRollEvent.ModifierRollResults is null)
        {
            throw new InvalidOperationException("There is no roll dice to reroll");
        }

        var reroll = successRollEvent.CreateReRollEvent([successRollEvent.RawRollResult], successRollEvent.ModifierRollResults);

        return Task.FromResult<IEvent>(reroll);
    }
}
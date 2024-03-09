namespace Dnd.Predefined.Definitions.Actions.Fighter;

using Dnd.Predefined.Actions;
using Dnd.Predefined.Events;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class IndomitableAction : Reaction, IRerollAction
{
    public IndomitableAction(ActionUsageLimit usageLimit)
        : base("Indomitable: Reroll Saving Throw", ActionDurationType.FreeAction, [usageLimit], EReactionType.After | EReactionType.Self | EReactionType.SavingThrow, false)
    {
    }

    public ERollType RollType => ERollType.Damage;

    public override async Task<bool> IsReactionAvailable(IGameActor gameActor, IActionEvent eventToReactTo, EReactionType reactionType)
    {
        return await base.IsReactionAvailable(gameActor, eventToReactTo, reactionType)
            && eventToReactTo is ISuccessRollEvent savingThrowEvent
            && eventToReactTo.Action is ISavingThrowAction
            && savingThrowEvent.RollResult.HasValue
            && savingThrowEvent.RollResult.Value.IsFailure();
    }

    public override IActionEvent CreateReactionEvent(IGameActor actor, IActionEvent eventToReactTo)
    {
        Task task = new(() =>
        {
            if (eventToReactTo is ISuccessRollEvent savingThrowEvent)
            {
                savingThrowEvent.ResetSuccessRoll();
            }
        });

        return new BasicReactionEvent(actor, eventToReactTo, this, task);
    }
}
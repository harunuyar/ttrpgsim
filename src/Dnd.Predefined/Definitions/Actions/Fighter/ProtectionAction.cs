namespace Dnd.Predefined.Definitions.Actions.Fighter;

using Dnd.Predefined.Actions;
using Dnd.Predefined.Events;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class ProtectionAction : Reaction
{
    public ProtectionAction()
        : base("Protection", ActionDurationType.Reaction, [], EReactionType.Before | EReactionType.AttackRoll | EReactionType.ToAlly | EReactionType.Nearby, false)
    {
    }

    public override async Task<bool> IsReactionAvailable(IGameActor gameActor, IActionEvent eventToReactTo, EReactionType reactionType)
    {
        return await base.IsReactionAvailable(gameActor, eventToReactTo, reactionType)
            && eventToReactTo is ISuccessRollEvent rollEvent
            && rollEvent.Action is IAttackRollAction;
    }

    public override IActionEvent CreateReactionEvent(IGameActor actor, IActionEvent eventToReactTo)
    {
        Task task = new(() =>
        {
            if (eventToReactTo is ISuccessRollEvent rollEvent)
            {
                rollEvent.RollAdvantages.AddValue(Name, EAdvantage.Disadvantage);
            }
        });

        return new BasicReactionEvent(actor, eventToReactTo, this, task);
    }
}

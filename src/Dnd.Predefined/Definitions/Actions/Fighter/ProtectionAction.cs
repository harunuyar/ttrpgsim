namespace Dnd.Predefined.Definitions.Actions.Fighter;

using Dnd.Predefined.Actions;
using Dnd.Predefined.Events;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class ProtectionAction : EventReaction
{
    public ProtectionAction()
        : base("Protection", ActionDurationType.Reaction, [], false)
    {
    }

    public override Task<bool> IsReactionAvailable(IGameActor gameActor, IEvent eventToReactTo)
    {
        return Task.FromResult(gameActor != eventToReactTo.EventOwner
            && eventToReactTo is ISuccessRollEvent rollEvent
            && rollEvent.SuccessRollAction.SuccessRollType is ESuccessRollType.Attack
            && rollEvent.EventPhase == EEventPhase.DoneRunning);
    }

    public override async Task<IEvent> CreateReactionEvent(IGameActor actor, IEvent eventToReactTo)
    {
        if (!await IsReactionAvailable(actor, eventToReactTo))
        {
            throw new InvalidOperationException("ProtectionAction.CreateReactionEvent: eventToReactTo is not attack roll");
        }

        Task task = new(() =>
        {
            if (eventToReactTo is ISuccessRollEvent rollEvent && rollEvent.RollAdvantages is not null)
            {
                rollEvent.RollAdvantages.AddValue(Name, EAdvantage.Disadvantage);
            }
        });

        return new BasicEvent(Name, actor, task);
    }
}

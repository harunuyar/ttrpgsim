namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class HealRollEvent : AEvent, IHealRollEvent
{
    public HealRollEvent(string name, IGameActor eventOwner, IHealAction healAction, IEnumerable<IGameActor> targets) : base(name, eventOwner)
    {
        HealAction = healAction;
        Targets = targets;
    }

    public IHealAction HealAction { get; }

    public IEnumerable<IGameActor> Targets { get; }

    public override Task<IEnumerable<IEvent>> RunEvent()
    {
        if (EventPhase != EEventPhase.WaitingOtherEvent)
        {
            throw new InvalidOperationException("Event is not in the correct phase to run.");
        }

        if (HealAction.TargetingType.TargetCount.HasValue && Targets.Count() != HealAction.TargetingType.TargetCount)
        {
            throw new InvalidOperationException("Incorrect number of targets for heal event.");
        }

        if (AmountRollEvent is null)
        {
            var amountEvent = new AmountRollEvent(EventName, EventOwner, HealAction, null);
            amountEvent.AddFinalAction(new Task(() => SetEventPhase(EEventPhase.Initialized)));

            SetEventPhase(EEventPhase.WaitingOtherEvent);
            return Task.FromResult<IEnumerable<IEvent>>([amountEvent]);
        }

        SetEventPhase(EEventPhase.DoneRunning);
        return Task.FromResult<IEnumerable<IEvent>>(Targets.Select(t => new HealEvent(EventName, t, HealAction, AmountRollEvent.AmountResult ?? 0)));
    }

    public AmountRollEvent? AmountRollEvent { get; private set; }

    public int? HealAmount => AmountRollEvent?.AmountResult;
}

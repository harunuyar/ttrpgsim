namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class HealRollEvent : ATargetingEvent
{
    public HealRollEvent(string name, IGameActor eventOwner, IHealAction healAction, IEnumerable<IGameActor> targets) 
        : base(name, eventOwner, healAction, targets)
    {
        HealAction = healAction;
    }

    public IHealAction HealAction { get; }

    public override Task<IEnumerable<IEvent>> RunEventImpl()
    {
        if (AmountRollEvent is null)
        {
            var amountEvent = new RollAmountEvent(EventName, EventOwner, HealAction, null);
            amountEvent.AddFinalAction(new Task(() => SetEventPhase(EEventPhase.Initialized)));

            SetEventPhase(EEventPhase.WaitingOtherEvent);
            return Task.FromResult<IEnumerable<IEvent>>([amountEvent]);
        }

        SetEventPhase(EEventPhase.DoneRunning);
        return Task.FromResult<IEnumerable<IEvent>>(Targets.Select(t => new HealEvent(EventName, t, HealAction, HealAmount ?? 0)));
    }

    public RollAmountEvent? AmountRollEvent { get; private set; }

    public int? HealAmount => AmountRollEvent?.AmountResult;
}

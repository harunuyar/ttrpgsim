namespace Dnd.Predefined.Events;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class SavingThrowDamageAttackEvent : ATargetingEvent
{
    public SavingThrowDamageAttackEvent(string name, IGameActor eventOwner, ISavingThrowDamageAttackAction savingThrowDamageAttackAction, IEnumerable<IGameActor> targets)
        : base(name, eventOwner, savingThrowDamageAttackAction, targets)
    {
        SavingThrowDamageAttackAction = savingThrowDamageAttackAction;
    }

    public ISavingThrowDamageAttackAction SavingThrowDamageAttackAction { get; }

    public RollAmountEvent? DamageRollEvent { get; private set; }

    public List<SavingThrowDamageEvent>? SavingThrowDamageEvents { get; private set; }

    public override Task<IEnumerable<IEvent>> RunEventImpl()
    {
        if (DamageRollEvent is null)
        {
            DamageRollEvent = new RollAmountEvent($"{EventName}: Damage Roll", EventOwner, SavingThrowDamageAttackAction, null);
            DamageRollEvent.AddFinalAction(new Task(() => { SetEventPhase(EEventPhase.Initialized); }));

            SetEventPhase(EEventPhase.WaitingOtherEvent);
            return Task.FromResult<IEnumerable<IEvent>>([DamageRollEvent]);
        }

        if (SavingThrowDamageEvents is null)
        {
            var amount = DamageRollEvent.AmountResult ?? throw new InvalidOperationException("Damage roll event doesn't have an amount result");

            SavingThrowDamageEvents = [];

            foreach (var Target in Targets)
            {
                var savingThrowDamageEvent = new SavingThrowDamageEvent(EventName, EventOwner, SavingThrowDamageAttackAction, amount, Target);
                savingThrowDamageEvent.AddFinalAction(new Task(NotifyDamageEventFinalized));

                SavingThrowDamageEvents.Add(savingThrowDamageEvent);
            }

            SetEventPhase(EEventPhase.WaitingOtherEvent);
            return Task.FromResult<IEnumerable<IEvent>>(SavingThrowDamageEvents);
        }

        SetEventPhase(EEventPhase.DoneRunning);
        return Task.FromResult<IEnumerable<IEvent>>([]);
    }

    private void NotifyDamageEventFinalized()
    {
        if (SavingThrowDamageEvents is null)
        {
            throw new InvalidOperationException("Saving throw damage events are not initialized");
        }

        if (SavingThrowDamageEvents.All(e => e.EventPhase == EEventPhase.Finalized))
        {
            SetEventPhase(EEventPhase.DoneRunning);
        }
    }
}

namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class AttackEvent : AEvent, IAttackEvent
{
    public AttackEvent(string name, IGameActor eventOwner, IAttackAction action, IGameActor? target) 
        : base(name, eventOwner)
    {
        AttackAction = action;
        Target = target;
    }

    public override bool IsWaitingForUserInput => Target is null;

    public IAttackAction AttackAction { get; }

    public IGameActor? Target { get; set; }

    public override Task<IEnumerable<IEvent>> RunEvent()
    {
        if (Target is null)
        {
            throw new InvalidOperationException("Target is not initialized");
        }

        if (EventPhase == EEventPhase.WaitingOtherEvent)
        {
            throw new InvalidOperationException("Event is waiting for other event");
        }

        if (DamageRollEvent is null)
        {
            DamageRollEvent = new AmountRollEvent($"{EventName}: Damage To {Target.Name} (Critical)", EventOwner, AttackAction, Target, false);
            DamageRollEvent.AddFinalAction(new Task(() => { SetEventPhase(EEventPhase.Initialized); }));

            SetEventPhase(EEventPhase.WaitingOtherEvent);
            return Task.FromResult<IEnumerable<IEvent>>([DamageRollEvent]);
        }
        else
        {
            int amount = DamageRollEvent.AmountResult ?? throw new InvalidOperationException("Damage roll event doesn't have an amount result");

            var damageEvent = new DamageEvent(EventName, Target, AttackAction, amount);

            SetEventPhase(EEventPhase.DoneRunning);
            return Task.FromResult<IEnumerable<IEvent>>([damageEvent]);
        }
    }

    public AmountRollEvent? DamageRollEvent { get; private set; }
}

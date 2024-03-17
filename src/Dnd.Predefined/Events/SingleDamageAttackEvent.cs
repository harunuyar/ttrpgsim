namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class SingleDamageAttackEvent : AEvent
{
    public SingleDamageAttackEvent(string name, IGameActor eventOwner, IDamageAction action, IGameActor? target, bool? critical) 
        : base(name, eventOwner)
    {
        AttackAction = action;
        Target = target;
        Critical = critical ?? false;
    }

    public override bool IsWaitingForUserInput => Target is null;

    public IDamageAction AttackAction { get; }

    public IGameActor? Target { get; set; }

    public bool Critical { get; set; }

    public override Task<IEnumerable<IEvent>> RunEventImpl()
    {
        if (DamageRollEvent is null)
        {
            DamageRollEvent = new RollAmountEvent($"{EventName}: Damage To {Target!.Name}", EventOwner, AttackAction, Target, Critical);
            DamageRollEvent.AddFinalAction(new Task(() => { SetEventPhase(EEventPhase.Initialized); }));

            SetEventPhase(EEventPhase.WaitingOtherEvent);
            return Task.FromResult<IEnumerable<IEvent>>([DamageRollEvent]);
        }
        else
        {
            int amount = DamageRollEvent.AmountResult ?? throw new InvalidOperationException("Damage roll event doesn't have an amount result");

            var damageEvent = new DamageEvent(EventName, Target!, AttackAction.DamageType, amount);
            damageEvent.AddFinalAction(new Task(() => { SetEventPhase(EEventPhase.DoneRunning); }));

            SetEventPhase(EEventPhase.WaitingOtherEvent);
            return Task.FromResult<IEnumerable<IEvent>>([damageEvent]);
        }
    }

    public RollAmountEvent? DamageRollEvent { get; private set; }
}

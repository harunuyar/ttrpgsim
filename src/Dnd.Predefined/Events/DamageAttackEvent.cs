namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class DamageAttackEvent : ASequenceTargettingEvent
{
    public DamageAttackEvent(string name, IGameActor eventOwner, IDamageAttackAction damageAttackAction, IEnumerable<IGameActor> targets, bool? critical) 
        : base(name, eventOwner, damageAttackAction, targets)
    {
        DamageAttackAction = damageAttackAction;
    }

    public IDamageAttackAction DamageAttackAction { get; }

    public bool? Critical { get; }

    public override Task<IEnumerable<IEvent>> CreateSubEvents(IGameActor target)
    {
        int no = (SubEvents?.Count ?? 0) + 1;
        var @event = new SingleDamageAttackEvent($"{EventName} {no}", EventOwner, DamageAttackAction, target, Critical);
        return Task.FromResult<IEnumerable<IEvent>>([@event]);
    }
}

namespace Dnd.Predefined.Events.AttackEvents;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class AttackRollDamageEvent : ASequenceTargettingEvent
{
    public AttackRollDamageEvent(string name, IGameActor eventOwner, IAttackRollDamageAction attackRollDamageAction, IEnumerable<IGameActor> targets) 
        : base(name, eventOwner, attackRollDamageAction, targets)
    {
        AttackRollDamageAction = attackRollDamageAction;
    }

    public IAttackRollDamageAction AttackRollDamageAction { get; }

    public override Task<IEnumerable<IEvent>> CreateSubEvents(IGameActor target)
    {
        int no = (SubEvents?.Count ?? 0) + 1;
        var @event = new SingleAttackRollDamageEvent($"{EventName} {no}", EventOwner, AttackRollDamageAction, target);
        return Task.FromResult<IEnumerable<IEvent>>([@event]);
    }
}

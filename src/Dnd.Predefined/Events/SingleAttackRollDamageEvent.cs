namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class SingleAttackRollDamageEvent : AttackRollEvent
{
    public SingleAttackRollDamageEvent(string name, IGameActor eventOwner, IAttackRollDamageAction attackRollDamageAction, IGameActor? target)
        : base(name, eventOwner, attackRollDamageAction, target)
    {
        AttackRollDamageAction = attackRollDamageAction;
    }

    public IAttackRollDamageAction AttackRollDamageAction { get; }

    public override Task<IEvent?> GetAttackEvent(ERollResult rollResult, IGameActor target)
    {
        if (rollResult.IsSuccess())
        {
            return Task.FromResult<IEvent?>(new SingleDamageAttackEvent($"{EventName}: Damage", EventOwner, AttackRollDamageAction, target, rollResult.IsCriticalSuccess()));
        }

        return Task.FromResult<IEvent?>(null);
    }
}

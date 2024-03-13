namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.DamageType;
using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.Events;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class AttackAction : DamageAction, IAttackAction
{
    public AttackAction(string name, ActionDurationType actionDurationType, ActionRange range, TargetingType targetingType, DamageTypeModel damageType, DicePool damageDicePool, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(name, actionDurationType, range, targetingType, damageType, damageDicePool, usageLimits)
    {
    }

    public EAttackType AttackType => Range.RangeType switch
    {
        ERangeType.Self => EAttackType.Melee,
        ERangeType.Touch => EAttackType.Melee,
        ERangeType.Ranged => EAttackType.Ranged,
        _ => EAttackType.Melee
    };

    public override Task<IEvent> CreateEvent(IGameActor actor)
    {
        if (TargetingType.TargetCount == 1)
        {
            return Task.FromResult<IEvent>(new AttackEvent(Name, actor, this, null));
        }
        else
        {
            return Task.FromResult<IEvent>(new MultipleAttackEvent(Name, actor, this, []));
        }
    }
}

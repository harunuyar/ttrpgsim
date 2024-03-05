namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.DamageType;
using Dnd._5eSRD.Models.Spell;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class AttackAction : DamageAction, IAttackAction
{
    public AttackAction(IGameActor actionOwner, string name, ActionDurationType actionDurationType, ActionRange range, TargetingType targetingType, DamageTypeModel damageType, DicePool damageDicePool, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(actionOwner, name, actionDurationType, range, targetingType, damageType, damageDicePool, usageLimits)
    {
    }

    public EAttackType AttackType => Range.RangeType switch
    {
        ERangeType.Self => EAttackType.Melee,
        ERangeType.Touch => EAttackType.Melee,
        ERangeType.Ranged => EAttackType.Ranged,
        _ => EAttackType.Melee
    };
}

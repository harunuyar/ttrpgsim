namespace Dnd.Predefined.Actions.ActionTypes;

using Dnd._5eSRD.Models.DamageType;
using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.Events.AttackEvents;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class AttackRollDamageAction : EventAction, IAttackRollDamageAction
{
    public AttackRollDamageAction(string name, ActionDurationType actionDurationType, IEnumerable<IActionUsageLimit> usageLimits,
        ActionRange range, TargetingType targetingType, DamageTypeModel damageType, DicePool damageDicePool)
        : base(name, actionDurationType, usageLimits)
    {
        DamageType = damageType;
        Range = range;
        TargetingType = targetingType;
        AmountDicePool = damageDicePool;
    }

    public ESuccessRollType SuccessRollType => ESuccessRollType.Attack;

    public EAmountRollType AmountRollType => EAmountRollType.Damage;

    public EAttackType AttackType => Range.RangeType switch
    {
        ERangeType.Self => EAttackType.Melee,
        ERangeType.Touch => EAttackType.Melee,
        ERangeType.Ranged => EAttackType.Ranged,
        _ => EAttackType.Melee
    };

    public ActionRange Range { get; }

    public TargetingType TargetingType { get; }

    public DamageTypeModel DamageType { get; }

    public DicePool AmountDicePool { get; }

    public override Task<IEvent> CreateEvent(IGameActor actor)
    {
        return Task.FromResult<IEvent>(new AttackRollDamageEvent(Name, actor, this, []));
    }
}

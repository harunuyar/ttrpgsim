namespace Dnd.Predefined.Actions.ActionTypes;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.DamageType;
using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.Events;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class SavingThrowDamageAttackAction : EventAction, ISavingThrowDamageAttackAction
{
    public SavingThrowDamageAttackAction(string name, ActionDurationType actionDurationType, IEnumerable<IActionUsageLimit> usageLimits,
        AbilityScoreModel ability, ActionRange range, TargetingType targetingType, DamageTypeModel damageType, DicePool damageDicePool,
        double successMultiplier = 0.5, double failureMultiplier = 1)
        : base(name, actionDurationType, usageLimits)
    {
        Ability = ability;
        Range = range;
        TargetingType = targetingType;
        DamageType = damageType;
        AmountDicePool = damageDicePool;
        SuccessDamageMultiplier = successMultiplier;
        FailureDamageMultiplier = failureMultiplier;
    }

    public AbilityScoreModel Ability { get; }

    public ESuccessRollType SuccessRollType => ESuccessRollType.Save;

    public EAttackType AttackType => Range.RangeType switch
    {
        ERangeType.Self => EAttackType.Melee,
        ERangeType.Touch => EAttackType.Melee,
        ERangeType.Ranged => EAttackType.Ranged,
        _ => EAttackType.Melee
    };

    public ActionRange Range { get; }

    public TargetingType TargetingType { get; }

    public double SuccessDamageMultiplier { get; }

    public double FailureDamageMultiplier { get; }

    public DamageTypeModel DamageType { get; }

    public DicePool AmountDicePool { get; }

    public EAmountRollType AmountRollType => EAmountRollType.Damage;

    public override Task<IEvent> CreateEvent(IGameActor actor)
    {
        return Task.FromResult<IEvent>(new SavingThrowDamageAttackEvent(Name, actor, this, []));
    }
}

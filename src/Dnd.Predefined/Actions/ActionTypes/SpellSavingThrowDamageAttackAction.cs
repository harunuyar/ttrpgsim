namespace Dnd.Predefined.Actions.ActionTypes;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd._5eSRD.Models.DamageType;
using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.Events;
using Dnd.Predefined.ModelExtensions;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;
using Dnd.System.GameManagers.Dice;

public class SpellSavingThrowDamageAttackAction : SpellEventAction, ISavingThrowDamageAttackAction
{
    public static async Task<SpellSavingThrowDamageAttackAction> Create(ISpellcastingAbility spellcastingAbility, SpellModel spellModel, int spellSlot, 
        IEnumerable<IActionUsageLimit> usageLimits, TargetingType targetingType, DicePool damageDicePool)
    {
        var damageTypeModel = spellModel.Damage?.DamageType is null ? null : await spellModel.Damage.DamageType.GetModel<DamageTypeModel>();
        var ability = spellModel.Dc?.DcType is null ? null : await spellModel.Dc.DcType.GetModel<AbilityScoreModel>();
        double successMultiplier = spellModel.Dc?.DcSuccess switch
        {
            EDcSuccess.None => 0,
            EDcSuccess.Half => 0.5,
            EDcSuccess.Full => 1,
            _ => 0.5
        };
        return damageTypeModel == null
            ? throw new InvalidOperationException("Spell does not have a damage type.")
            : ability == null
                ? throw new InvalidOperationException("Spell does not have a saving throw ability.")
                : new SpellSavingThrowDamageAttackAction(
                        spellcastingAbility, spellModel, spellSlot, usageLimits, ability, targetingType, damageTypeModel, damageDicePool, successMultiplier);
    }

    private SpellSavingThrowDamageAttackAction(ISpellcastingAbility spellcastingAbility, SpellModel spellModel, int spellSlot, IEnumerable<IActionUsageLimit> usageLimits,
        AbilityScoreModel ability, TargetingType targetingType, DamageTypeModel damageType, DicePool damageDicePool,
        double successMultiplier = 0.5, double failureMultiplier = 1)
        : base(spellcastingAbility, spellModel, spellSlot, usageLimits)
    {
        Ability = ability;
        Range = ActionRange.FromString(spellModel.Range) ?? ActionRange.Self;
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

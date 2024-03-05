namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.DamageType;
using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.ModelExtensions;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.Entities.Instances;
using Dnd.System.GameManagers.Dice;

public class AttackSpellAction : SpellAction, IAttackSpellAction
{
    public static async Task<AttackSpellAction> Create(IGameActor actionOwner, ISpellcastingAbility spellcastingAbility, SpellModel spellModel, TargetingType targetingType, int castingLevel, IEnumerable<IActionUsageLimit> usageLimits)
    {
        var damageTypeModel = spellModel.Damage?.DamageType is null ? null : await spellModel.Damage.DamageType.GetModel<DamageTypeModel>();
        return damageTypeModel == null
            ? throw new InvalidOperationException("Spell does not have a damage type.")
            : new AttackSpellAction(actionOwner, spellcastingAbility, spellModel, targetingType, damageTypeModel, castingLevel, usageLimits);
    }

    public AttackSpellAction(IGameActor actionOwner, ISpellcastingAbility spellcastingAbility, SpellModel spellModel, TargetingType targetingType, DamageTypeModel damageTypeModel, int castingLevel, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(actionOwner, spellcastingAbility, spellModel, castingLevel, usageLimits)
    {
        DamageAction = new DamageAction(
            actionOwner, 
            Name, 
            ActionDuration, 
            ActionRange.FromString(spellModel.Range) ?? ActionRange.Self, 
            targetingType, 
            damageTypeModel, 
            spellModel.GetDamage(actionOwner.LevelInfo.Level, castingLevel),
            []);
    }

    private DamageAction DamageAction { get; }

    public DamageTypeModel DamageType => DamageAction.DamageType;

    public ActionRange Range => DamageAction.Range;

    public TargetingType TargetingType => DamageAction.TargetingType;

    public DicePool AmountDicePool => DamageAction.AmountDicePool;

    public Task<EAdvantage> GetAmountAdvantage()
    {
        return DamageAction.GetAmountAdvantage();
    }

    public Task<DicePool> GetAmountBonus()
    {
        return DamageAction.GetAmountBonus();
    }

    public Task<int> GetAmountResult(int defaultAmount)
    {
        return DamageAction.GetAmountResult(defaultAmount);
    }

    public Task<int?> GetPredeterminedAmount()
    {
        return DamageAction.GetPredeterminedAmount();
    }
}

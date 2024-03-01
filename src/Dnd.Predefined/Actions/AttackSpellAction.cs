namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.DamageType;
using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.ModelExtensions;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class AttackSpellAction : SpellAction, IAttackSpellAction
{
    public static async Task<AttackSpellAction> Create(IGameActor actionOwner, SpellModel spellModel, TargetingType targetingType, int castingLevel)
    {
        var damageTypeModel = spellModel.Damage?.DamageType is null ? null : await spellModel.Damage.DamageType.GetModel<DamageTypeModel>();
        return damageTypeModel == null
            ? throw new InvalidOperationException("Spell does not have a damage type.")
            : new AttackSpellAction(actionOwner, spellModel, targetingType, damageTypeModel, castingLevel);
    }

    public AttackSpellAction(IGameActor actionOwner, SpellModel spellModel, TargetingType targetingType, DamageTypeModel damageTypeModel, int castingLevel) 
        : base(actionOwner, spellModel, castingLevel)
    {
        DamageAction = new DamageAction(
            actionOwner, 
            Name, 
            ActionDuration, 
            ActionRange.FromString(spellModel.Range) ?? ActionRange.Self, 
            targetingType, 
            damageTypeModel, 
            spellModel.GetDamage(actionOwner.LevelInfo.Level, castingLevel));
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

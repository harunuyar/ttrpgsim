namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.DamageType;
using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.ModelExtensions;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Instances;
using Dnd.System.GameManagers.Dice;

public class AttackSpellAction : SpellAction, IAttackSpellAction
{
    public static async Task<AttackSpellAction> Create(ISpellcastingAbility spellcastingAbility, SpellModel spellModel, TargetingType targetingType, int charLevel, int castingLevel, IEnumerable<IActionUsageLimit> usageLimits)
    {
        var damageTypeModel = spellModel.Damage?.DamageType is null ? null : await spellModel.Damage.DamageType.GetModel<DamageTypeModel>();
        return damageTypeModel == null
            ? throw new InvalidOperationException("Spell does not have a damage type.")
            : new AttackSpellAction(spellcastingAbility, spellModel, targetingType, damageTypeModel, charLevel, castingLevel, usageLimits);
    }

    public AttackSpellAction(ISpellcastingAbility spellcastingAbility, SpellModel spellModel, TargetingType targetingType, DamageTypeModel damageTypeModel, int charLevel, int castingLevel, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(spellcastingAbility, spellModel, castingLevel, usageLimits)
    {
        DamageAction = new DamageAction(
            Name, 
            ActionDuration, 
            ActionRange.FromString(spellModel.Range) ?? ActionRange.Self, 
            targetingType, 
            damageTypeModel,
            spellModel.GetDamage(charLevel, castingLevel),
            []);
    }

    private DamageAction DamageAction { get; }

    public DamageTypeModel DamageType => DamageAction.DamageType;

    public ActionRange Range => DamageAction.Range;

    public TargetingType TargetingType => DamageAction.TargetingType;

    public DicePool AmountDicePool => DamageAction.AmountDicePool;
}

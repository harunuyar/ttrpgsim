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
    public static async Task<AttackSpellAction> Create(ILevelInfo levelInfo, ISpellcastingAbility spellcastingAbility, SpellModel spellModel, TargetingType targetingType, int spellSlot, IEnumerable<IActionUsageLimit> usageLimits)
    {
        var damageTypeModel = spellModel.Damage?.DamageType is null ? null : await spellModel.Damage.DamageType.GetModel<DamageTypeModel>();
        return damageTypeModel == null
            ? throw new InvalidOperationException("Spell does not have a damage type.")
            : new AttackSpellAction(levelInfo, spellcastingAbility, spellModel, targetingType, damageTypeModel, spellSlot, usageLimits);
    }

    public AttackSpellAction(ILevelInfo levelInfo, ISpellcastingAbility spellcastingAbility, SpellModel spellModel, TargetingType targetingType, DamageTypeModel damageTypeModel, int spellSlot, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(spellcastingAbility, spellModel, spellSlot, usageLimits)
    {
        AttackAction = new AttackAction(
            Name, 
            ActionDuration, 
            ActionRange.FromString(spellModel.Range) ?? ActionRange.Self, 
            targetingType, 
            damageTypeModel,
            new DicePool([], 0),
            []);
        LevelInfo = levelInfo;
    }

    private ILevelInfo LevelInfo { get; }

    private AttackAction AttackAction { get; }

    public DamageTypeModel DamageType => AttackAction.DamageType;

    public ActionRange Range => AttackAction.Range;

    public TargetingType TargetingType => AttackAction.TargetingType;

    public DicePool AmountDicePool => Spell.GetDamage(LevelInfo.Level, SpellSlot);

    public EAmountRollType AmountRollType => AttackAction.AmountRollType;

    public EAttackType AttackType => AttackAction.AttackType;
}

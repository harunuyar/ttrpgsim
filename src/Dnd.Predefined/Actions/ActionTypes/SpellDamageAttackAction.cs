namespace Dnd.Predefined.Actions.ActionTypes;

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

public class SpellDamageAttackAction : SpellEventAction, IDamageAttackAction
{
    public static async Task<SpellDamageAttackAction> Create(ILevelInfo levelInfo, ISpellcastingAbility spellcastingAbility, SpellModel spellModel, TargetingType targetingType, int spellSlot, IEnumerable<IActionUsageLimit> usageLimits)
    {
        var damageTypeModel = spellModel.Damage?.DamageType is null ? null : await spellModel.Damage.DamageType.GetModel<DamageTypeModel>();
        return damageTypeModel == null
            ? throw new InvalidOperationException("Spell does not have a damage type.")
            : new SpellDamageAttackAction(levelInfo, spellcastingAbility, spellModel, targetingType, damageTypeModel, spellSlot, usageLimits);
    }

    public SpellDamageAttackAction(ILevelInfo levelInfo, ISpellcastingAbility spellcastingAbility, SpellModel spellModel, TargetingType targetingType, DamageTypeModel damageTypeModel, int spellSlot, IEnumerable<IActionUsageLimit> usageLimits)
        : base(spellcastingAbility, spellModel, spellSlot, usageLimits)
    {
        LevelInfo = levelInfo;
        DamageType = damageTypeModel;
        Range = ActionRange.FromString(spellModel.Range) ?? ActionRange.Self;
        TargetingType = targetingType;
    }

    private ILevelInfo LevelInfo { get; }

    public DamageTypeModel DamageType { get; }

    public ActionRange Range { get; }

    public TargetingType TargetingType { get; }

    public DicePool AmountDicePool => Spell.GetDamage(LevelInfo.Level, SpellSlot);

    public EAmountRollType AmountRollType => EAmountRollType.Damage;

    public EAttackType AttackType => Range.RangeType switch
    {
        ERangeType.Self => EAttackType.Melee,
        ERangeType.Touch => EAttackType.Melee,
        ERangeType.Ranged => EAttackType.Ranged,
        _ => EAttackType.Melee
    };

    public override Task<IEvent> CreateEvent(IGameActor actor)
    {
        return Task.FromResult<IEvent>(new DamageAttackEvent(Name, actor, this, [], null));
    }
}

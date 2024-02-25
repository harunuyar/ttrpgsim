namespace Dnd.Predefined.Spells.Categories;

using Dnd._5eSRD.Models.Spell;
using Dnd.Predefined.ModelExtensions;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class DirectDamageSpell : ASpell, IAttackAction
{
    public DirectDamageSpell(IGameActor actionOwner, SpellModel spell, TargetingType targetingType, ESuccessMeasuringType successMeasuringType) : base(actionOwner, spell, targetingType)
    {
        SuccessMeasuringType = successMeasuringType;
    }

    public EAttackType AttackType => Spell.AttackType ?? EAttackType.None;

    public ESuccessMeasuringType SuccessMeasuringType { get; }

    public DicePool DamageDicePool => Spell.Level == 0 ? DamageAtCharacterLevel[ActionOwner!.LevelInfo.Level] : DamageAtSlotLevel[Spell.Level!.Value];

    public Dictionary<int, DicePool> DamageAtSlotLevel => Spell.GetDamageAtSlotLevel();

    public Dictionary<int, DicePool> DamageAtCharacterLevel => Spell.GetDamageAtCharacterLevel();

    EAttackType IAttackAction.AttackType => throw new NotImplementedException();
}
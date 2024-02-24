namespace Dnd.System.Entities.Action;

using Dnd._5eSRD.Models.Spell;
using Dnd.System.Entities.Instances;
using Dnd.System.GameManagers.Dice;

public enum EAttackType
{
    MainHandMelee,
    OffHandMelee,
    UnarmedMelee,
    Ranged,
    MeleeSpell,
    RangedSpell,
    Special
}

public enum EAttackResult
{
    Hit,
    Miss,
    CriticalHit,
    CriticalMiss
}

public enum EDamageCalculationType
{
    Constant,
    DiceRoll,
}

public enum ESuccessMeasuringType
{
    AttackRoll,
    SavingThrow,
    Guaranteed
}

public interface IAttackAction : IAction
{
    EAttackType AttackType { get; }
    ESuccessMeasuringType SuccessMeasuringType { get; }
    EDamageCalculationType DamageCalculationType { get; }
    DiceRoll? DamageRoll { get; }
    int? ConstantDamage { get; }
    EquipmentInstance? Weapon { get; }
    SpellModel? Spell { get; }
}

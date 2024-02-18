namespace Dnd.System.Entities.Actions;

using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Damage;

public interface IAttacking : IBonusProvider
{
    EDamageType DamageType { get; }

    ESuccessMeasuringType SuccessMeasuringType { get; }

    EDamageCalculationType DamageCalculationType { get; }

    int? ConstantDamage { get; }

    DiceRoll? DamageDie { get; }

    EAttributeType? SavingThrowAttribute { get; }

    Func<int, int>? FailureDamageModifier { get; }
}

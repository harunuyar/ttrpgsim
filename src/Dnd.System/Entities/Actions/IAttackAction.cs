namespace Dnd.System.Entities.Actions;

using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Damage;

public interface IAttackAction
{
    EDamageType DamageType { get; }

    ESuccessMeasuringType SuccessMeasuringType { get; }

    EDamageCalculationType DamageCalculationType { get; }

    int? ConstantDamage { get; }

    DiceRoll? DamageDie { get; }
}

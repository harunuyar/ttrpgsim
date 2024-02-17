namespace Dnd.System.Entities.Spells;

using Dnd.GameManagers.Dice;
using Dnd.System.Entities.DiceModifiers;
using Dnd.System.Entities.Damage;

public interface IAttackingSpell : ISpell
{
    ESuccessMeasuringType SuccessMeasuringType { get; }

    public EDamageType DamageType { get; }

    public EDamageCalculationType DamageCalculationType { get; }

    public int ConstantDamage { get; }

    public DiceRoll? DamageDie { get; }

    int GetDamage(ERollResult rollSuccess);
}

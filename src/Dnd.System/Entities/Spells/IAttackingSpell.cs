namespace Dnd.System.Entities.Spells;

using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Items.Equipments.Weapons;

public interface IAttackingSpell : ISpell
{
    public EDamageType DamageType { get; }

    public EDamageCalculationType DamageCalculationType { get; }

    public int ConstantDamage { get; }

    public DiceRoll? DamageDie { get; }
}

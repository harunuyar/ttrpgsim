namespace Dnd.System.Entities.Items.Equipments.Weapons;

using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Damage;

public interface IWeapon : IItemDescription
{
    public EWeaponType WeaponType { get; }

    public EDamageType DamageType { get; }

    public EWeaponProperty WeaponProperties { get; }

    public ESuccessMeasuringType SuccessMeasuringType { get; }

    public EDamageCalculationType DamageCalculationType { get; }

    public int ConstantDamage { get; }

    public DiceRoll? DamageDie { get; }

    public DiceRoll? VersatileDamageDie { get; }
}

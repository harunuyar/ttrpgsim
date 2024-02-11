namespace Dnd.Entities.Items.Equipments.Weapons;

using Dnd.CommandSystem.Commands;
using Dnd.Entities.Units;
using Dnd.GameManagers.Dice;

public abstract class AWeapon : IItemDescription
{
    public AWeapon(string name, string desc, Weight weight, Value value, EWeaponType weaponType, EDamageCalculationType damageCalculationType, EDamageType damageType, EWeaponProperty weaponProperties, ESuccessMeasuringType actionType, int constantDamage = 0, DiceRoll? damageDie = null)
    {
        Name = name;
        Description = desc;
        Weight = weight;
        Value = value;
        WeaponType = weaponType;
        DamageDie = damageDie;
        DamageType = damageType;
        WeaponProperties = weaponProperties;
        IsIdentified = true;
        SuccessMeasuringType = actionType;
        ConstantDamage = constantDamage;
        DamageCalculationType = damageCalculationType;
    }

    public bool IsIdentified { get; set; }

    public bool IsStackable => false;

    public bool IsConsumable => false;

    public bool IsEquippable => true;

    public string Name { get; set; }

    public string Description { get; set; }

    public Weight Weight { get; set;}

    public Value Value { get; set; }

    public EWeaponType WeaponType { get; set; }

    public EDamageCalculationType DamageCalculationType { get; set; }

    public int ConstantDamage { get; set; }

    public DiceRoll? DamageDie { get; set; }

    public EDiceType VersatileDamageDie { get; set; }

    public EDamageType DamageType { get; set; }

    public EWeaponProperty WeaponProperties { get; set; }

    public ESuccessMeasuringType SuccessMeasuringType { get; set; }

    public virtual void HandleCommand(DndCommand command)
    {
    }
}

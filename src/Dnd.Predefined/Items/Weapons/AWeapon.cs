namespace Dnd.Predefined.Items.Weapons;

using Dnd.System.Entities.Units;
using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Damage;
using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.Actions;
using Dnd.System.Entities.Attributes;

public abstract class AWeapon : IWeapon
{
    public AWeapon(
        string name, 
        string desc, 
        Weight weight, 
        Value value, 
        EWeaponType weaponType, 
        EDamageCalculationType damageCalculationType, 
        EDamageType damageType, 
        EWeaponProperty weaponProperties, 
        ESuccessMeasuringType actionType, 
        int? constantDamage = null, 
        DiceRoll? damageDie = null, 
        DiceRoll? versatileDamageDie = null)
    {
        Name = name;
        Description = desc;
        Weight = weight;
        Value = value;
        WeaponType = weaponType;
        DamageDie = damageDie;
        VersatileDamageDie = versatileDamageDie;
        DamageType = damageType;
        WeaponProperties = weaponProperties;
        SuccessMeasuringType = actionType;
        ConstantDamage = constantDamage;
        DamageCalculationType = damageCalculationType;
    }

    public bool IsStackable => false;

    public bool IsConsumable => false;

    public bool IsEquippable => true;

    public string Name { get; }

    public string Description { get; }

    public Weight Weight { get;}

    public Value Value { get; }

    public EWeaponType WeaponType { get; }

    public EDamageCalculationType DamageCalculationType { get; }

    public int? ConstantDamage { get; }

    public DiceRoll? DamageDie { get; }

    public DiceRoll? VersatileDamageDie { get; }

    public EDamageType DamageType { get; }

    public EWeaponProperty WeaponProperties { get; }

    public ESuccessMeasuringType SuccessMeasuringType { get; }

    public EAttributeType? SavingThrowAttribute => null;

    public Func<int, int>? FailureDamageModifier => null;

    public virtual void HandleCommand(ICommand command)
    {
    }
}

namespace DnD.Entities.Items.Equipments.Weapons;

using DnD.CommandSystem.Commands;
using DnD.Entities.Units;
using DnD.GameManagers.Dice;

internal abstract class AWeapon : IItemDescription
{
    public AWeapon(string name, string desc, Weight weight, Value value, EDiceType damageDie, EDamageType damageType, EWeaponProperty weaponProperties)
    {
        Name = name;
        Description = desc;
        Weight = weight;
        Value = value;
        DamageDie = damageDie;
        DamageType = damageType;
        WeaponProperties = weaponProperties;
    }

    public bool IsStackable => false;

    public bool IsConsumable => false;

    public bool IsEquippable => true;

    public string Name { get; set; }

    public string Description { get; set; }

    public Weight Weight { get; set;}

    public Value Value { get; set; }

    public EDiceType DamageDie { get; set; }

    public EDiceType VersatileDamageDie { get; set; }

    public EDamageType DamageType { get; set; }

    public EWeaponProperty WeaponProperties { get; set; }

    public virtual void HandleCommand(DndCommand command)
    {
    }
}

namespace Dnd.System.Entities.Items.Equipments.Weapons;

using Dnd.GameManagers.Dice;
using Dnd.System.Entities.Actions;

public interface IWeapon : IItemDescription, IAttackAction
{
    EWeaponType WeaponType { get; }

    EWeaponProperty WeaponProperties { get; }

    DiceRoll? VersatileDamageDie { get; }
}

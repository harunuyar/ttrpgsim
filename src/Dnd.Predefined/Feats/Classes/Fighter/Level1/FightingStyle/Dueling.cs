namespace Dnd.Predefined.Feats.Classes.Fighter.Level1.FightingStyle;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.Entities.Actions.Impl;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class Dueling : AFeat, IFightingStyle
{
    public Dueling() : base("Dueling", "When you are wielding a melee weapon in one hand and no other weapons, you gain a +2 bonus to damage rolls with that weapon.")
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetDamageModifier getDamageModifier
            && getDamageModifier.AttackAction is WeaponAttack weaponAttack
            && weaponAttack.Weapon.WeaponType.IsMelee()
            && !weaponAttack.Weapon.WeaponProperties.HasFlag(EWeaponProperty.TwoHanded)
            && command.Actor.Inventory.Equipments.MainHandWeapon != null
            && command.Actor.Inventory.Equipments.OffHandWeapon == null)
        {
            getDamageModifier.AddBonus(this, 2);
        }
    }
}

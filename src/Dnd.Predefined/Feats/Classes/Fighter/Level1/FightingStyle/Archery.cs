namespace Dnd.Predefined.Feats.Classes.Fighter.Level1.FightingStyle;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Items.Equipments.Weapons;

internal class Archery : AFeat, IFightingStyle
{
    public Archery() : base("Archery", "You gain +2 bonus to attack rolls you make with ranged weapons.")
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetWeaponAttackModifier calculateWeaponAttackModifier
            && calculateWeaponAttackModifier.WeaponItem.ItemDescription is IWeapon weapon
            && weapon.WeaponType.IsRanged())
        {
            calculateWeaponAttackModifier.AddBonus(this, 2);
        }
    }
}

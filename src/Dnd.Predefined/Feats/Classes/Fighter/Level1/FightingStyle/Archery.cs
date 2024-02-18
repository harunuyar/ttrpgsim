namespace Dnd.Predefined.Feats.Classes.Fighter.Level1.FightingStyle;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
using Dnd.System.Entities.Actions.Impl;
using Dnd.System.Entities.Items.Equipments.Weapons;

internal class Archery : AFeat, IFightingStyle
{
    public Archery() : base("Archery", "You gain +2 bonus to attack rolls you make with ranged weapons.")
    {
    }

    public override void HandleCommand(ICommand command)
    {
        if (command is GetAttackModifier getAttackModifier
            && getAttackModifier.AttackAction is WeaponAttack weaponAttack
            && weaponAttack.Weapon.WeaponType.IsRanged())
        {
            getAttackModifier.AddBonus(this, 2);
        }
    }
}

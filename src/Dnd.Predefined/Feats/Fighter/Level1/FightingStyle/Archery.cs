namespace Dnd.Predefined.Feats.Fighter.Level1.FightingStyle;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands;
using Dnd.System.Entities.Items.Equipments.Weapons;

internal class Archery : AFeat, IFightingStyle
{
    public Archery() : base("Archery", "You gain +2 bonus to attack rolls you make with ranged weapons.")
    {
    }

    public override void HandleCommand(DndCommand command)
    {
        if (command is CalculateWeaponAttackModifier calculateWeaponAttackModifier 
            && calculateWeaponAttackModifier.WeaponItem.ItemDescription is IWeapon weapon 
            && weapon.WeaponType.IsRanged())
        {
            calculateWeaponAttackModifier.Result.BonusCollection.AddBonus(this, 2);
        }
    }
}

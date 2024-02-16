namespace Dnd.Predefined.Traits.Dwarf;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class DwarvenCombatTraining : ATrait
{
    public static readonly DwarvenCombatTraining Instance = new DwarvenCombatTraining();

    private DwarvenCombatTraining() : base("Dwarven Combat Training", "You have proficiency with the battleaxe, handaxe, light hammer, and warhammer.")
    {
    }

    public EWeaponType WeaponProficiency => EWeaponType.Battleaxe | EWeaponType.Handaxe | EWeaponType.LightHammer | EWeaponType.Warhammer;

    public override void HandleCommand(ICommand command)
    {
        if (command is HasWeaponProficiency getWeaponProficiency)
        {
            if (WeaponProficiency.HasFlag(getWeaponProficiency.WeaponType))
            {
                getWeaponProficiency.SetValue(this, true, $"You have {getWeaponProficiency.WeaponType} proficiency.");
            }
        }
    }
}

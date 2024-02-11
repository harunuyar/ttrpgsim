namespace Dnd.Entities.Traits.Dwarf;

using Dnd.CommandSystem.Commands;
using Dnd.CommandSystem.Commands.BooleanResultCommands;
using Dnd.Entities.Items.Equipments.Weapons;

public class DwarvenCombatTraining : ATrait
{
    public static readonly DwarvenCombatTraining Instance = new DwarvenCombatTraining();

    private DwarvenCombatTraining() : base("Dwarven Combat Training", "You have proficiency with the battleaxe, handaxe, light hammer, and warhammer.")
    {
    }

    public EWeaponType WeaponProficiency => EWeaponType.Battleaxe | EWeaponType.Handaxe | EWeaponType.LightHammer | EWeaponType.Warhammer;

    public override void HandleCommand(DndCommand command)
    {
        if (command is HasWeaponProficiency getWeaponProficiency)
        {
            if (WeaponProficiency.HasFlag(getWeaponProficiency.WeaponType))
            {
                getWeaponProficiency.Result.SetValue(this, true);
            }
        }
    }
}

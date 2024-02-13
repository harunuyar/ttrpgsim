namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class HasWeaponProficiency : DndBooleanCommand
{
    public HasWeaponProficiency(ICharacter character, EWeaponType weaponType) : base(character)
    {
        this.WeaponType = weaponType;
    }

    public EWeaponType WeaponType { get; }

    protected override void InitializeResult()
    {
        Result.SetValue("Default", false);
    }

    protected override void FinalizeResult()
    {
    }
}

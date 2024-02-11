namespace Dnd.CommandSystem.Commands.BooleanResultCommands;

using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Weapons;

public class HasWeaponProficiency : DndBooleanCommand
{
    public HasWeaponProficiency(Character character, EWeaponType weaponType) : base(character)
    {
        this.WeaponType = weaponType;
    }

    public EWeaponType WeaponType { get; }

    public override void InitializeResult()
    {
        Result.SetValue("Base", this.Character.HasWeaponProficiency(WeaponType));
    }
}

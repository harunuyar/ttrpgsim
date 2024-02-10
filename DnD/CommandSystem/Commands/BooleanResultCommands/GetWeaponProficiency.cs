namespace DnD.CommandSystem.Commands.BooleanResultCommands;

using DnD.Entities.Characters;
using DnD.Entities.Items.Equipments.Weapons;

internal class GetWeaponProficiency : DndBooleanCommand
{
    public GetWeaponProficiency(Character character, EWeaponType weaponType) : base(character)
    {
        this.WeaponType = weaponType;
    }

    public EWeaponType WeaponType { get; }

    public override void InitializeResult()
    {
        Result.SetValue("Base", this.Character.HasWeaponProficiency(WeaponType));
    }
}

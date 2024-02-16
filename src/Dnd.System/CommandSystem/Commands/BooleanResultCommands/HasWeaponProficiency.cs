namespace Dnd.System.CommandSystem.Commands.BooleanResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items.Equipments.Weapons;
using Dnd.System.Entities.Skills;

public class HasWeaponProficiency : DndBooleanCommand
{
    public HasWeaponProficiency(IGameActor character, EWeaponType weaponType) : base(character)
    {
        this.WeaponType = weaponType;
    }

    public EWeaponType WeaponType { get; }

    protected override void InitializeResult()
    {
        Result.SetValue(false, $"{Actor.Name} doesn't have {WeaponType} proficiency.");
    }
}

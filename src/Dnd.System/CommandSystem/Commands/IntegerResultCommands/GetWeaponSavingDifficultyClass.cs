namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class GetWeaponSavingDifficultyClass : DndScoreCommand
{
    public GetWeaponSavingDifficultyClass(IGameActor character, IItem weaponItem) : base(character)
    {
        WeaponItem = weaponItem;
    }

    public IItem WeaponItem { get; }

    protected override void InitializeResult()
    {
        if (WeaponItem.ItemDescription is not IWeapon weapon)
        {
            SetErrorAndReturn("Item is not a weapon");
            return;
        }

        if (weapon.SuccessMeasuringType != Entities.ESuccessMeasuringType.SavingThrow)
        {
            SetErrorAndReturn("Weapon doesn't use saving throw as success measuring type");
            return;
        }

        Result.SetBaseValue("Base", 8);
    }
}

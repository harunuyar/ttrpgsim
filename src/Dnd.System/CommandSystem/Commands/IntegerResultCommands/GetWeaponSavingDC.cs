namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.BooleanResultCommands;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Items;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class GetWeaponSavingDC : DndScoreCommand
{
    public GetWeaponSavingDC(IGameActor character, IItem weaponItem) : base(character)
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

        var hasProficiency = new HasWeaponProficiency(Actor, weapon.WeaponType).Execute();

        if (!hasProficiency.IsSuccess)
        {
            SetErrorAndReturn("HasWeaponProficiency: " + hasProficiency.ErrorMessage);
            return;
        }

        if (hasProficiency.Value)
        {
            var proficiencyBonus = new GetProficiencyBonus(Actor).Execute();

            if (!proficiencyBonus.IsSuccess)
            {
                SetErrorAndReturn("GetProficiencyBonus: " + proficiencyBonus.ErrorMessage);
                return;
            }

            Result.AddAsBonus("Proficiency", proficiencyBonus);
        }
    }
}

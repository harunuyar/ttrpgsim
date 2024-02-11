namespace Dnd.CommandSystem.Commands.IntegerResultCommands;

using Dnd.Entities.Characters;
using Dnd.Entities.Items.Equipments.Weapons;

public class CalculateWeaponSavingDifficultyClass : DndScoreCommand
{
    public CalculateWeaponSavingDifficultyClass(Character character, AWeapon weapon) : base(character)
    {
        Weapon = weapon;
    }

    public AWeapon Weapon { get; }

    public override void InitializeResult()
    {
        if (Weapon.SuccessMeasuringType == Entities.ESuccessMeasuringType.SavingThrow)
        {
            Result.SetBaseValue("Base", 8);

            var getProficiencyBonus = new GetProficiencyBonus(this.Character);
            var proficiencyBonus = getProficiencyBonus.Execute();

            if (proficiencyBonus.IsSuccess)
            {
                Result.BonusCollection.AddBonus("Proficiency Bonus", proficiencyBonus.Value);
            }
            else
            {
                Result.SetError(proficiencyBonus.ErrorMessage ?? "Couldn't get proficiency bonus");
            }
        }
        else
        {
            Result.SetError("Weapon doesn't use saving throw as success measuring type");
        }
    }
}

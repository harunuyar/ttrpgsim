namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Characters;
using Dnd.System.Entities.Items.Equipments.Weapons;

public class CalculateWeaponSavingDifficultyClass : DndScoreCommand
{
    public CalculateWeaponSavingDifficultyClass(ICharacter character, IWeapon weapon) : base(character)
    {
        Weapon = weapon;
    }

    public IWeapon Weapon { get; }

    protected override void InitializeResult()
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

    protected override void FinalizeResult()
    {
    }
}

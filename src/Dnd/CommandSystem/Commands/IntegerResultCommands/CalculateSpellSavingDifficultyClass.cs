namespace Dnd.CommandSystem.Commands.IntegerResultCommands;

using Dnd.Entities;
using Dnd.Entities.Characters;
using Dnd.Entities.Spells;

public class CalculateSpellSavingDifficultyClass : DndScoreCommand
{
    public CalculateSpellSavingDifficultyClass(Character character, ISpell spell) : base(character)
    {
        Spell = spell;
    }

    public ISpell Spell { get; }

    public override void InitializeResult()
    {
        if (Spell.SuccessMeasuringType == ESuccessMeasuringType.SavingThrow)
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

            // Attribute bonus will be provided by spell casting ability feature
        }
        else
        {
            Result.SetError("Spell doesn't require saving throw");
        }
    }
}

namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities;
using Dnd.System.Entities.GameActors;
using Dnd.System.Entities.Spells;

public class CalculateSpellSavingDifficultyClass : DndScoreCommand
{
    public CalculateSpellSavingDifficultyClass(IGameActor character, ISpell spell) : base(character)
    {
        Spell = spell;
    }

    public ISpell Spell { get; }

    protected override void InitializeResult()
    {
        if (Spell.SpellDescription.SuccessMeasuringType == ESuccessMeasuringType.SavingThrow)
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

    protected override void FinalizeResult()
    {
    }
}

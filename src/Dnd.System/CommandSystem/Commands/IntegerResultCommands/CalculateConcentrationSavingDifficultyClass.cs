namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Characters;

public class CalculateConcentrationSavingDifficultyClass : DndScoreCommand
{
    public CalculateConcentrationSavingDifficultyClass(ICharacter character, int damageTaken) : base(character)
    {
        DamageTaken = damageTaken;
    }

    public int DamageTaken { get; set; }

    public override void InitializeResult()
    {
        int damageDC = DamageTaken / 2;
        if (damageDC > 10)
        {
            Result.SetBaseValue("Half of Damage Taken", damageDC);
        }
        else
        {
            Result.SetBaseValue("Base", 10);
        }
    }
}

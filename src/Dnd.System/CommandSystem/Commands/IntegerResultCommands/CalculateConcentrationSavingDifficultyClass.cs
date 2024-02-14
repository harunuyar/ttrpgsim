namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.GameActors;

public class CalculateConcentrationSavingDifficultyClass : DndScoreCommand
{
    public CalculateConcentrationSavingDifficultyClass(IGameActor character, int damageTaken) : base(character)
    {
        DamageTaken = damageTaken;
    }

    public int DamageTaken { get; set; }

    protected override void InitializeResult()
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

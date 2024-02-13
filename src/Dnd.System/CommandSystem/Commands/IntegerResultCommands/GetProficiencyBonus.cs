namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.GameActors;

public class GetProficiencyBonus : DndScoreCommand
{
    private static readonly int[] ProficiencyBonusArr = new[] { 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6 };

    public GetProficiencyBonus(IGameActor character) : base(character)
    {
    }

    protected override void InitializeResult()
    {
        int level = Character.LevelInfo.Level;

        if (level < 1 || level > 20)
        {
            Result.SetError("Level must be between 1 and 20");
        }
        else
        {
            Result.SetBaseValue("Base", ProficiencyBonusArr[level - 1]);
        }
    }

    protected override void FinalizeResult()
    {
    }
}

namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetProficiencyBonus : ScoreCommand
{
    private static readonly int[] ProficiencyBonusArr = [2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6];

    public GetProficiencyBonus(IGameActor character) : base(character)
    {
    }

    protected override Task InitializeResult()
    {
        int level = Actor.LevelInfo.Level;

        if (level < 1 || level > 20)
        {
            SetError("Level must be between 1 and 20, but it is " + level);
        }
        else
        {
            SetBaseValue(ProficiencyBonusArr[level - 1], "Proficiency Bonus");
        }

        return Task.CompletedTask;
    }
}

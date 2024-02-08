namespace DnD.Commands;

using DnD.Entities.Characters;
using TableTopRpg.Commands;

internal class GetProficiencyBonusCommand : DndCommand
{
    private static readonly int[] ProficiencyBonusArr = new[] { 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6 };

    public GetProficiencyBonusCommand(Character character) : base(character)
    {
    }

    protected override ICommandResult ExecuteDndCommand()
    {
        int level = Character.Level;

        if (level < 1 || level > 20)
        {
            return IntegerResult.Failure(this, "Level must be between 1 and 20");
        }
        else
        {
            return SummarizedIntegerResult.Success(this, ProficiencyBonusArr[level - 1], "Base");
        }
    }
}

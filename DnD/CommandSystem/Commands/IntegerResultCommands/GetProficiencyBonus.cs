namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.CommandSystem.Results;
using DnD.Entities.Characters;

internal class GetProficiencyBonus : DndScoreCommand
{
    private static readonly int[] ProficiencyBonusArr = new[] { 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6 };

    public GetProficiencyBonus(Character character) : base(character)
    {
    }

    public override IntegerResultWithBonuses Execute()
    {
        int level = Character.Level;

        if (level < 1 || level > 20)
        {
            return IntegerResultWithBonuses.Failure(this, "Level must be between 1 and 20");
        }
        else
        {
            return IntegerResultWithBonuses.Success(this, "Base", ProficiencyBonusArr[level - 1], IntegerBonuses);
        }
    }
}

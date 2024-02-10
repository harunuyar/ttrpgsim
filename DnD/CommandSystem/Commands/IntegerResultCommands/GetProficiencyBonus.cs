namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.Entities.Characters;

internal class GetProficiencyBonus : DndScoreCommand
{
    private static readonly int[] ProficiencyBonusArr = new[] { 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6 };

    public GetProficiencyBonus(Character character) : base(character)
    {
    }

    public override void InitializeResult()
    {
        int level = Character.Level;

        if (level < 1 || level > 20)
        {
            Result.SetError("Level must be between 1 and 20");
        }
        else
        {
            Result.SetBaseValue("Base", ProficiencyBonusArr[level - 1]);
        }
    }
}

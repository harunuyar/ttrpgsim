namespace DnD.CommandSystem.Commands.IntegerResultCommands;

using DnD.Entities.Attributes;
using DnD.Entities.Characters;

internal class GetMaxHP : DndScoreCommand
{
    public GetMaxHP(Character character) : base(character)
    {
    }

    public override void InitializeResult()
    {
        Result.SetBaseValue("Base", Character.HitPoints.HitPointRolls.Sum());

        var getConstModifier = new GetAttributeModifier(Character, EAttributeType.Constitution);
        var constModifierResult = getConstModifier.Execute();

        if (constModifierResult.IsSuccess)
        {
            Result.SetBaseValue(Character.AttributeSet.Constitution, Character.Level * constModifierResult.Value);
        }
    }
}

namespace Dnd.CommandSystem.Commands.IntegerResultCommands;

using Dnd.Entities.Attributes;
using Dnd.Entities.Characters;

public class GetMaxHP : DndScoreCommand
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
            Result.BonusCollection.AddBonus(Character.AttributeSet.Constitution, Character.Level * constModifierResult.Value);
        }
    }
}

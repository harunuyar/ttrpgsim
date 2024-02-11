namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.Characters;

public class GetMaxHP : DndScoreCommand
{
    public GetMaxHP(ICharacter character) : base(character)
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

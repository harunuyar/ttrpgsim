namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.Entities.Attributes;
using Dnd.System.Entities.GameActors;

public class GetMaxHP : DndScoreCommand
{
    public GetMaxHP(IGameActor character) : base(character)
    {
        ShouldSetFullHealth = character.HitPoints.CurrentHitPoints == character.HitPoints.MaxHitPoints;
    }

    public bool ShouldSetFullHealth { get; set; }

    protected override void InitializeResult()
    {
        Result.SetBaseValue("Base", Character.HitPoints.HitPointRolls.Sum());

        var getConstModifier = new GetAttributeModifier(Character, EAttributeType.Constitution);
        var constModifierResult = getConstModifier.Execute();

        if (constModifierResult.IsSuccess)
        {
            Result.BonusCollection.AddBonus(Character.AttributeSet.Constitution, Character.LevelInfo.Level * constModifierResult.Value);
        }
    }

    protected override void FinalizeResult()
    {
        if (Result.IsSuccess)
        {
            Character.HitPoints.MaxHitPoints = Result.Value;
            Character.HitPoints.SetCurrentHitPoints(ShouldSetFullHealth ? Result.Value : Math.Min(Character.HitPoints.CurrentHitPoints, Result.Value));
        }
    }
}

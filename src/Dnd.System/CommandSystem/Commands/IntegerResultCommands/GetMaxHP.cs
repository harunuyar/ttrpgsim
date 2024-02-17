namespace Dnd.System.CommandSystem.Commands.IntegerResultCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.CommandSystem.Commands.IntegerResultCommands.Modifiers;
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
        Result.SetBaseValue("Base", Actor.HitPoints.HitPointRolls.Sum());

        var constModifierResult = new GetAttributeModifier(Actor, EAttributeType.Constitution).Execute();

        if (!constModifierResult.IsSuccess)
        {
            SetErrorAndReturn("GetAttributeModifier: " + constModifierResult.ErrorMessage);
            return;
        }

        Result.BonusCollection.AddBonus(Actor.AttributeSet.Constitution, Actor.LevelInfo.Level * constModifierResult.Value);
    }

    protected override void FinalizeResult()
    {
        if (Result.IsSuccess)
        {
            Actor.HitPoints.MaxHitPoints = Result.Value;
            Actor.HitPoints.SetCurrentHitPoints(ShouldSetFullHealth ? Result.Value : Math.Min(Actor.HitPoints.CurrentHitPoints, Result.Value));
        }
    }
}

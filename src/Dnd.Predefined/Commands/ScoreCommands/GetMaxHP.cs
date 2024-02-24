namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd.Context;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetMaxHP : ScoreCommand
{
    public GetMaxHP(IGameActor character) : base(character)
    {
    }

    protected override async Task InitializeResult()
    {
        SetBaseValue(Actor.HitPoints.HitPointRolls.Sum(), "Base");

        var abilityScore = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Con);
        if (abilityScore == null)
        {
            SetError("AbilityScoreModel not found for " + AbilityScores.Dex);
            return;
        }

        var constModifierResult = await new GetAbilityModifier(Actor, abilityScore).Execute();

        if (!constModifierResult.IsSuccess)
        {
            SetError("GetAbilityModifier: " + constModifierResult.ErrorMessage);
            return;
        }

        AddBonus(Actor.LevelInfo.Level * constModifierResult.Value, "Constitution");
    }

    protected override Task FinalizeResult()
    {
        if (Result.IsSuccess)
        {
            Actor.HitPoints.SetMaxHitPoints(Result.Value);
        }

        return Task.CompletedTask;
    }
}

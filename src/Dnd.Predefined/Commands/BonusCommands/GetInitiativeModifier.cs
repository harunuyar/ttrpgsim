namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd.Context;
using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetInitiativeModifier : ListCommand<int>
{
    public GetInitiativeModifier(IGameActor character) : base(character)
    {
    }

    protected override async Task InitializeResult()
    {
        var abilityScore = await DndContext.Instance.GetObject<AbilityScoreModel>(AbilityScores.Dex);
        if (abilityScore == null)
        {
            SetError("AbilityScoreModel not found for " + AbilityScores.Dex);
            return;
        }

        var dexterityModifierResult = await new GetAbilityModifier(Actor, abilityScore).Execute();

        if (!dexterityModifierResult.IsSuccess)
        {
            SetError("GetAbilityModifier: " + dexterityModifierResult.ErrorMessage);
        }

        AddValue(dexterityModifierResult.Value, dexterityModifierResult.Message);
    }
}

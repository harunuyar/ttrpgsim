namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd._5eSRD.Constants;
using Dnd._5eSRD.Models.AbilityScore;
using Dnd.Context;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetArmorClass : ScoreCommand
{
    public GetArmorClass(IGameActor character) : base(character)
    {
    }

    protected override async Task InitializeResult()
    {
        SetBaseValue(10, "Base Armor Class");

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

        AddBonus(dexterityModifierResult);
    }
}

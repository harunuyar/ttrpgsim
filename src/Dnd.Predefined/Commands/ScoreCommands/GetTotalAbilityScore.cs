namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetTotalAbilityScore : ScoreCommand
{
    public GetTotalAbilityScore(IGameActor character, AbilityScoreModel abilityScore) : base(character)
    {
        AbilityScore = abilityScore;
    }

    public AbilityScoreModel AbilityScore { get; }

    protected override async Task InitializeResult()
    {
        var getBaseAttributeScore = await new GetBaseAbilityScore(Actor, AbilityScore).Execute();

        if (!getBaseAttributeScore.IsSuccess)
        {
            SetError("GetBaseAbilityScore: " + getBaseAttributeScore.ErrorMessage);
            return;
        }

        Set(getBaseAttributeScore);
    }
}

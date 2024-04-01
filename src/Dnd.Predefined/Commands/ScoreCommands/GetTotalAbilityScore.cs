namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetTotalAbilityScore : ScoreCommand
{
    public GetTotalAbilityScore(IGameActor character, AbilityScoreModel abilityScore) : base(character)
    {
        Ability = abilityScore;
    }

    public AbilityScoreModel Ability { get; }

    protected override async Task InitializeResult()
    {
        var getBaseAttributeScore = await new GetBaseAbilityScore(Actor, Ability).Execute();

        if (!getBaseAttributeScore.IsSuccess)
        {
            SetError("GetBaseAbilityScore: " + getBaseAttributeScore.ErrorMessage);
            return;
        }

        Set(getBaseAttributeScore);
    }
}

namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetAbilityModifier : ScoreCommand
{
    public GetAbilityModifier(IGameActor character, AbilityScoreModel ability) : base(character)
    {
        Ability = ability;
    }

    public AbilityScoreModel Ability { get; }

    protected override async Task InitializeResult()
    {
        var attributeScoreResult = await new GetTotalAbilityScore(Actor, Ability).Execute();

        if (!attributeScoreResult.IsSuccess)
        {
            SetError("GetTotalAbilityScore: " + attributeScoreResult.ErrorMessage);
            return;
        }

        SetBaseValue(attributeScoreResult.Value / 2 - 5, Ability.FullName ?? string.Empty);
    }
}

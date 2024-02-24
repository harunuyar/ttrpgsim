namespace Dnd.Predefined.Commands.ScoreCommands;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetBaseAbilityScore : ScoreCommand
{
    public GetBaseAbilityScore(IGameActor character, AbilityScoreModel ability) : base(character)
    {
        Ability = ability;
    }

    public AbilityScoreModel Ability { get; }

    protected override Task InitializeResult()
    {
        SetBaseValue(Actor.AttributeSet.GetAbilityScore(Ability), Ability.FullName ?? string.Empty);

        return Task.CompletedTask;
    }
}

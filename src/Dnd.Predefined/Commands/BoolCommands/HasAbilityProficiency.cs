namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class HasAbilityProficiency : ValueCommand<bool>
{
    public HasAbilityProficiency(IGameActor character, AbilityScoreModel ability) : base(character)
    {
        Ability = ability;
    }

    public AbilityScoreModel Ability { get; }

    protected override async Task InitializeResult()
    {
        var proficiency = await new HasProficiency(Actor, Ability).Execute();
        if (!proficiency.IsSuccess)
        {
            SetError("HasProficiency: " + proficiency.ErrorMessage);
            return;
        }

        Set(proficiency);
    }
}

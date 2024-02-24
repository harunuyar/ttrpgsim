namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities;
using Dnd.System.Entities.GameActor;

public class HasSavingThrowProficiency : ValueCommand<bool>
{
    public HasSavingThrowProficiency(IGameActor character, AbilityScoreModel ability) : base(character)
    {
        Ability = ability;
    }

    public AbilityScoreModel Ability { get; }

    protected override async Task InitializeResult()
    {
        if (Ability.Url == null)
        {
            SetError($"{Ability.Name} equipment url is null.");
        }

        if (await Actor.HasProficiency(Ability.Url!))
        {
            SetValue(true, $"{Actor.Name} has {Ability.FullName} proficiency.");
            return;
        }

        SetValue(false, $"{Actor.Name} doesn't have {Ability} saving throw proficiency.");
    }
}

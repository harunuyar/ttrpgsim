namespace Dnd.Predefined.Commands.ValueCommands;

using Dnd._5eSRD.Models.AbilityScore;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class GetSpellCasterAttribute : ValueCommand<AbilityScoreModel>
{
    public GetSpellCasterAttribute(IGameActor character) : base(character)
    {
    }
}

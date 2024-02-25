namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetAdvantageForAbilityCheck : ListCommand<EAdvantage>
{
    public GetAdvantageForAbilityCheck(IGameActor actor, IAbilityCheckAction abilityCheck) : base(actor)
    {
        AbilityCheck = abilityCheck;
    }

    public IAbilityCheckAction AbilityCheck { get; }
}

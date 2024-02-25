namespace Dnd.Predefined.Commands.BonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetAdvantageForSkillCheck : ListCommand<EAdvantage>
{
    public GetAdvantageForSkillCheck(IGameActor actor, ISkillCheckAction skillCheck) : base(actor)
    {
        SkillCheck = skillCheck;
    }

    public ISkillCheckAction SkillCheck { get; }

    protected override async Task InitializeResult()
    {
        var abilityCheck = await new GetAdvantageForAbilityCheck(Actor, SkillCheck).Execute();

        if (!abilityCheck.IsSuccess)
        {
            SetError("GetAdvantageForAbilityCheck: " + abilityCheck.ErrorMessage);
            return;
        }

        Add(abilityCheck);
    }
}

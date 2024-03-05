namespace Dnd.Predefined.Actions;

using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class SuccessRollAction : RollAction, ISuccessRollAction
{
    public SuccessRollAction(IGameActor actionOwner, string name, ActionDurationType actionDurationType, ERollType rollType, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(actionOwner, name, actionDurationType, rollType, usageLimits)
    {
    }

    public async Task<ERollResult> GetPredeterminedResult()
    {
        var result = await new GetPredeterminedRollResult(ActionOwner, this, null).Execute();

        if (!result.IsSuccess)
        {
            throw new InvalidOperationException("GetPredeterminedRollResult: " + result.ErrorMessage);
        }

        return result.Values.Select(x => x.Item2).DefaultIfEmpty(ERollResult.None).Aggregate((a, b) => a | b);
    }

    public async Task<ERollResult> GetResult(ERollResult defaultResult)
    {
        var result = await new GetRollActionResult(ActionOwner, this, null, defaultResult).Execute();

        if (!result.IsSuccess)
        {
            throw new InvalidOperationException("GetActionResult: " + result.ErrorMessage);
        }

        return result.Values.Select(x => x.Item2).DefaultIfEmpty(defaultResult).Aggregate((a, b) => a | b);
    }
}

namespace Dnd.Predefined.Actions;

using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;

public class SuccessRollAction : RollAction, ISuccessRollAction
{
    public SuccessRollAction(string name, ActionDurationType actionDurationType, ERollType rollType, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(name, actionDurationType, rollType, usageLimits)
    {
    }
}

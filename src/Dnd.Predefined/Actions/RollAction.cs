namespace Dnd.Predefined.Actions;

using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;

public class RollAction : Action, IRollAction
{
    public RollAction(string name, ActionDurationType actionDurationType, ERollType rollType, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(name, actionDurationType, usageLimits)
    {
        RollType = rollType;
    }

    public ERollType RollType { get; }
}

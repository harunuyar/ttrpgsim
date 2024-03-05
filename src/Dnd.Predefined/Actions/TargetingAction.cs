namespace Dnd.Predefined.Actions;

using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;

public class TargetingAction : Action, ITargetingAction
{
    public TargetingAction(string name, ActionDurationType actionDurationType, ActionRange range, TargetingType targetingType, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(name, actionDurationType, usageLimits)
    {
        Range = range;
        TargetingType = targetingType;
    }

    public ActionRange Range { get; }

    public TargetingType TargetingType { get; }
}

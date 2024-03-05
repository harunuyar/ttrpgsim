namespace Dnd.Predefined.Actions;

using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class TargetingAction : Action, ITargetingAction
{
    public TargetingAction(IGameActor actionOwner, string name, ActionDurationType actionDurationType, ActionRange range, TargetingType targetingType, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(actionOwner, name, actionDurationType, usageLimits)
    {
        Range = range;
        TargetingType = targetingType;
    }

    public ActionRange Range { get; }

    public TargetingType TargetingType { get; }
}

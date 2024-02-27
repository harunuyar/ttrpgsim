namespace Dnd.Predefined.Actions;

using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class ATargetingAction : AAction, ITargetingAction
{
    public ATargetingAction(IGameActor actionOwner, string name, ActionDurationType actionDurationType, ActionRange range, TargetingType targetingType) 
        : base(actionOwner, name, actionDurationType)
    {
        Range = range;
        TargetingType = targetingType;
    }

    public ActionRange Range { get; }

    public TargetingType TargetingType { get; }
}

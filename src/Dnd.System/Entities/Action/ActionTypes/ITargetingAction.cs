namespace Dnd.System.Entities.Action.ActionTypes;

public interface ITargetingAction : IAction
{
    ActionRange Range { get; }
    TargetingType TargetingType { get; }
}

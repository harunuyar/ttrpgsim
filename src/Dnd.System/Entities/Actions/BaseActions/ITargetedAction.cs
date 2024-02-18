using Dnd.System.Entities.Actions.BaseActions;

namespace Dnd.System.Entities.Actions;

public interface ITargetedAction : IAction
{
    int TargetCount { get; }
}

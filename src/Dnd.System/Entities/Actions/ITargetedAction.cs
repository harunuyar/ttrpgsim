namespace Dnd.System.Entities.Actions;

public interface ITargetedAction : IAction
{
    int TargetCount { get; }
}

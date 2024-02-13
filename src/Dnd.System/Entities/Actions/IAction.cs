namespace Dnd.System.Entities.Actions;

public interface IAction : IDndEntity
{
    string Description { get; }

    EActionType ActionType { get; }
}

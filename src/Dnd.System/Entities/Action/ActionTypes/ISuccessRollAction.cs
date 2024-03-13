namespace Dnd.System.Entities.Action.ActionTypes;

public interface ISuccessRollAction : IAction
{
    ESuccessRollType SuccessRollType { get; }
}

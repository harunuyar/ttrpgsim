namespace Dnd.System.Entities.Action;

using Dnd.System.Entities.GameActor;

public interface IAction
{
    EActionType ActionType { get; }
    IGameActor ActionOwner { get; }
}

namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public interface IReaction : IAction
{
    EReactionType ReactionType { get; }
    bool MandatoryReaction { get; }
    Task<bool> IsReactionAvailable(IGameActor gameActor, IActionEvent eventToReactTo, EReactionType reactionType);
    IActionEvent CreateReactionEvent(IGameActor gameActor, IActionEvent eventToReactTo);
}

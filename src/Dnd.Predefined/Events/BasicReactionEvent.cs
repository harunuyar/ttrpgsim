namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class BasicReactionEvent : BasicActionEvent, IReactionEvent
{
    public BasicReactionEvent(IGameActor gameActor, IActionEvent eventToReactTo, IAction action, Task task) : base(gameActor, action, task)
    {
        EventToReactTo = eventToReactTo;
    }

    public IActionEvent EventToReactTo { get; }
}

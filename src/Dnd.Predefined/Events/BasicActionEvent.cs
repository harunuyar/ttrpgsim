namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class BasicActionEvent : IActionEvent
{
    public BasicActionEvent(IGameActor gameActor, IAction action, Task task)
    {
        Actor = gameActor;
        Action = action;
        Task = task;
    }

    public IGameActor Actor { get; }

    public IAction Action { get; }

    public Task Task { get; }

    public async Task FinalizeEvent()
    {
        await Task;
    }
}

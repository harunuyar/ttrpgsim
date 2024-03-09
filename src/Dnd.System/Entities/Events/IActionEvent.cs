namespace Dnd.System.Entities.Events;

using Dnd.System.Entities.Action.ActionTypes;

public interface IActionEvent : IEvent
{
    IAction Action { get; }
}

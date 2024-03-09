namespace Dnd.System.Entities.Events;

public interface IReactionEvent : IActionEvent
{
    IActionEvent EventToReactTo { get; }
}

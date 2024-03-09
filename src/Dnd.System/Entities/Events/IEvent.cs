namespace Dnd.System.Entities.Events;

using Dnd.System.Entities.GameActor;

public interface IEvent
{
    IGameActor Actor { get; }
    Task FinalizeEvent();
}

namespace Dnd.Predefined.Events;

using Dnd.System.Entities.GameActor;

public class TurnBeginEvent : AEvent
{
    public TurnBeginEvent(IGameActor eventOwner) : base("Turn Begin", eventOwner)
    {
    }
}

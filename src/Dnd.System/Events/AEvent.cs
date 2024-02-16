namespace Dnd.System.Events;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Events.EventListener;

public abstract class AEvent
{
    public AEvent(IEventListener eventListener)
    {
        EventListener = eventListener;
    }

    public IEventListener EventListener { get; }

    public abstract BooleanResult IsValid();

    public abstract EventResult QuickRun();
}

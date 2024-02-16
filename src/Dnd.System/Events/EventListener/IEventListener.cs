namespace Dnd.System.Events.EventListener;

using Dnd.System.CommandSystem.Results;

public interface IEventListener
{
    void OnEventResult(EventResult eventResult);
}

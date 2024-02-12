namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;

public interface IEventListener
{
    void OnEventResult(EventResult eventResult);
}

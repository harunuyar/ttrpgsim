namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class BasicEvent : AEvent
{
    public BasicEvent(string name, IGameActor actor, Task task) : base(name, actor)
    {
        Task = task;
    }

    public Task Task { get; }

    public override async Task<IEnumerable<IEvent>> RunEvent()
    {
        await Task;
        return await base.RunEvent();
    }
}

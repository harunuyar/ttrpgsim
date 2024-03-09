namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Effect;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class BasicEffectEvent : IEffectEvent
{
    public BasicEffectEvent(IEffectDefinition effectDefinition, IGameActor actor, IGameActor target, Task task)
    {
        EffectDefinition = effectDefinition;
        Actor = actor;
        Task = task;
        Target = target;
    }

    public IEffectDefinition EffectDefinition { get; }

    public IGameActor Actor { get; }

    public Task Task { get; }

    public IGameActor Target { get; }

    public async Task FinalizeEvent()
    {
        await Task;
    }
}

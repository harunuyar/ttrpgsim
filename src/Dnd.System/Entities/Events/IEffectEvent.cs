namespace Dnd.System.Entities.Events;

using Dnd.System.Entities.Effect;
using Dnd.System.Entities.GameActor;

public interface IEffectEvent : IEvent
{
    IEffectDefinition EffectDefinition { get; }

    IGameActor Target { get; }
}

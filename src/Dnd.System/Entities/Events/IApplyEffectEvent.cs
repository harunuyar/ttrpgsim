namespace Dnd.System.Entities.Events;

using Dnd.System.Entities.Action.ActionTypes;

public interface IApplyEffectEvent : IEvent
{
    IEffectAction EffectAction { get; }
}

namespace Dnd.System.Entities.Events;

using Dnd.System.Entities.Action.ActionTypes;

public interface IDamageEvent : IEvent
{
    IDamageAction DamageAction { get; }
    int Amount { get; }
}

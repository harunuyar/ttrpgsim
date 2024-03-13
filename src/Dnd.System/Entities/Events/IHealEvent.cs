namespace Dnd.System.Entities.Events;

using Dnd.System.Entities.Action.ActionTypes;

public interface IHealEvent : IEvent
{
    IHealAction? HealAction { get; }
    int Amount { get; }
}

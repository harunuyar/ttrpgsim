namespace Dnd.System.Entities.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public interface IAttackEvent : IEvent
{
    IAttackAction AttackAction { get; }

    // Initialized by user
    IGameActor? Target { get; }
}

namespace Dnd.System.Entities.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public interface IMultipleAttackEvent : IEvent
{
    IAttackAction AttackAction { get; }
    List<IGameActor> Targets { get; }
}

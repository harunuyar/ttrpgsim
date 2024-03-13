namespace Dnd.System.Entities.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public interface IMultipleAttackRollEvent : IEvent
{
    IAttackRollAction AttackRollAction { get; }
    List<IGameActor> Targets { get; }
}

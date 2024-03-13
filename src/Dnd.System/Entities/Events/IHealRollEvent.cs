namespace Dnd.System.Entities.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public interface IHealRollEvent
{
    IHealAction HealAction { get; }

    // Initialized by user
    IEnumerable<IGameActor> Targets { get; }

    // Initialized by RunEvent
    int? HealAmount { get; }
}

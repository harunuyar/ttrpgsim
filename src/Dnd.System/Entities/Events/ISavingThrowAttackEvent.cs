namespace Dnd.System.Entities.Events;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public interface ISavingThrowAttackEvent
{
    ISavingThrowAttackAction SavingThrowAttackAction { get; }

    // Initialized by user
    HashSet<IGameActor> Targets { get; }

    // Initialized in InitializeEvent method
    ScoreResult? SaveDc { get; }

    // Initialized in RunEvent method
    IAmountRollEvent? DamageRollEvent { get; }
    IEnumerable<ISuccessRollEvent>? SavingThrowEvents { get; }
}

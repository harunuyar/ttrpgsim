namespace Dnd.System.Entities.Events;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.GameManagers.Dice;

public interface ISuccessRollEvent : IEvent
{
    // Initialized with constructor
    ISuccessRollAction SuccessRollAction { get; }
    int? TargetResult { get; }

    // Initialized with RunEvent method
    ListResult<DicePool>? Modifiers { get; }
    ListResult<EAdvantage>? Advantages { get; }
    DiceRollResult? RawRollResult { get; }
    IEnumerable<DiceRollResult>? ModifierRollResults { get; }
    int? ModifierConstantResult { get; }
    ListResult<ERollResult>? PredeterminedSuccessResults { get; }
    int? TotalResult { get; }
    ERollResult? RollResult { get; }
    ListResult<ERollResult>? PostDeterminedSuccessResults { get; }

    IEvent CreateReRollEvent(IEnumerable<DiceRollResult> prevRolls, IEnumerable<DiceRollResult> prevModifierRolls);
}

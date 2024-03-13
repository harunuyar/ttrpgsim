namespace Dnd.System.Entities.Events;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.GameManagers.Dice;

public interface ISuccessRollEvent : IEvent
{
    // Initialized with constructor
    ISuccessRollAction SuccessRollAction { get; }
    int? TargetResult { get; }

    // Initialized with InitializeEvent method
    ListResult<DicePool>? RollModifiers { get; }
    ListResult<EAdvantage>? RollAdvantages { get; }
    ListResult<ERollResult>? PredeterminedRollResults { get; }

    // Initialized with RunEvent method
    ListResult<ERollResult>? RollResultModifiers { get; }
    DiceRollResult? RawRollResult { get; }
    IEnumerable<DiceRollResult>? ModifierRollResults { get; }
    int? ConstantModifier { get; }
    int? TotalResult { get; }
    ERollResult? RollResult { get; set; }
}

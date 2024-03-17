namespace Dnd.System.Entities.Events;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.GameManagers.Dice;

public interface IAmountRollEvent : IEvent
{
    // Initialized with constructor
    IAmountAction AmountAction { get; }
    bool Critical { get; }

    // Initialized with RunEvent method
    ListResult<DicePool>? Modifiers { get; }
    ListResult<EAdvantage>? Advantages { get; }
    IEnumerable<DiceRollResult>? RawRollResults { get; }
    IEnumerable<DiceRollResult>? ModifierRollResults { get; }
    int? ModifierConstantResult { get; }
    int? RawConstantResult { get; }
    ValueResult<int?>? PredeterminedAmountResult { get; }
    int? AmountResult { get; }

    IEvent CreateReRollEvent(IEnumerable<DiceRollResult> prevRolls, IEnumerable<DiceRollResult> prevModifierRolls);
}

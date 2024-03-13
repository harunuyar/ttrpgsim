namespace Dnd.System.Entities.Events;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.GameManagers.Dice;

public interface IAmountRollEvent : IEvent
{
    // Initialized with constructor
    IAmountAction AmountAction { get; }
    bool Critical { get; }

    // Initialized with InitializeEvent method
    ListResult<DicePool>? AmountModifiers { get; }
    ListResult<EAdvantage>? AmountAdvantages { get; }
    ValueResult<int?>? PredeterminedAmountResult { get; }
    int? RawConstantResult { get; }
    int? ConstantModifier { get; }

    // Initialized with RunEvent method
    IEnumerable<DiceRollResult>? RawRollResult { get; }
    IEnumerable<DiceRollResult>? ModifierRollResults { get; }
    int? AmountResult { get; }
}

namespace Dnd.System.Entities.Events;

using Dnd.System.CommandSystem.Results;
using Dnd.System.GameManagers.Dice;

public interface IAmountRollEvent : IActionEvent
{
    ListResult<DicePool> AmountModifiers { get; }
    ListResult<EAdvantage> AmountAdvantages { get; }
    ValueResult<int?> PredeterminedAmountResult { get; }
    int? RawAmountResult { get; set; }
    int? AmountResult { get; set; }

    void ResetAmountRoll()
    {
        RawAmountResult = null;
        AmountResult = null;
    }
}

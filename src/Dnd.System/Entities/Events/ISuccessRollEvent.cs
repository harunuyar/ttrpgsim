namespace Dnd.System.Entities.Events;

using Dnd.System.CommandSystem.Results;
using Dnd.System.GameManagers.Dice;

public interface ISuccessRollEvent : IActionEvent
{
    ListResult<DicePool> RollModifiers { get; }
    ListResult<EAdvantage> RollAdvantages { get; }
    ListResult<ERollResult> PredeterminedRollResults { get; }
    int? RawRollResult { get; set; }
    ERollResult? RollResult { get; set; }

    void ResetSuccessRoll()
    {
        RawRollResult = null;
        RollResult = null;
    }
}

namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.GameManagers.Dice;

public interface IAmountAction : IAction
{
    DicePool AmountDicePool { get; }
    Task<DicePool> GetAmountBonus();
    Task<EAdvantage> GetAmountAdvantage();
    Task<int?> GetPredeterminedAmount();
    Task<int> GetAmountResult(int defaultAmount);
}

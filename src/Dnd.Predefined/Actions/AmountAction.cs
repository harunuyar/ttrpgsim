namespace Dnd.Predefined.Actions;

using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.GameManagers.Dice;

public class AmountAction : Action, IAmountAction
{
    public AmountAction(string name, ActionDurationType actionDurationType, DicePool amountDicePool, IEnumerable<IActionUsageLimit> usageLimits)
        : base(name, actionDurationType, usageLimits)
    {
        AmountDicePool = amountDicePool;
    }

    public DicePool AmountDicePool { get; }
}

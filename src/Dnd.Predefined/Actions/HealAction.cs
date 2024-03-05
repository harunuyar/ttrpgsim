namespace Dnd.Predefined.Actions;

using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.GameManagers.Dice;

public class HealAction : TargetingAction, IHealAction
{
    public HealAction(string name, ActionDurationType actionDurationType, ActionRange range, TargetingType targetingType, DicePool healDicePool, IEnumerable<IActionUsageLimit> usageLimits)
        : base(name, actionDurationType, range, targetingType, usageLimits)
    {
        AmountAction = new AmountAction( name, actionDurationType, healDicePool, []);
    }

    public AmountAction AmountAction { get; }

    public DicePool AmountDicePool => AmountAction.AmountDicePool;
}

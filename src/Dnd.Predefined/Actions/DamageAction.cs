namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.DamageType;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.GameManagers.Dice;

public class DamageAction : TargetingAction, IDamageAction
{
    public DamageAction(string name, ActionDurationType actionDurationType, ActionRange range, TargetingType targetingType, DamageTypeModel damageType, DicePool damageDicePool, IEnumerable<IActionUsageLimit> usageLimits)
        : base(name, actionDurationType, range, targetingType, usageLimits)
    {
        DamageType = damageType;
        AmountAction = new AmountAction(name, actionDurationType, damageDicePool, EAmountRollType.Damage, []);
    }

    public AmountAction AmountAction { get; }

    public DamageTypeModel DamageType { get; }

    public DicePool AmountDicePool => AmountAction.AmountDicePool;

    public EAmountRollType AmountRollType => AmountAction.AmountRollType;
}

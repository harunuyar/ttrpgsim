namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.DamageType;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.GameManagers.Dice;

public abstract class AttackRollAction : AttackAction, IAttackRollAction
{
    public AttackRollAction(string name, ActionDurationType actionDurationType, ActionRange range, TargetingType targetingType, DamageTypeModel damageType, DicePool damageDicePool, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(name, actionDurationType, range, targetingType, damageType, damageDicePool, usageLimits)
    {
        SuccessRollAction = new SuccessRollAction(Name, actionDurationType, ERollType.Attack, []);
    }

    private SuccessRollAction SuccessRollAction { get; }

    public ERollType RollType => SuccessRollAction.RollType;
}

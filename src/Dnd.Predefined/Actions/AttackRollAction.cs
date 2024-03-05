namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.DamageType;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public abstract class AttackRollAction : AttackAction, IAttackRollAction
{
    public AttackRollAction(IGameActor actionOwner, string name, ActionDurationType actionDurationType, ActionRange range, TargetingType targetingType, DamageTypeModel damageType, DicePool damageDicePool, IEnumerable<IActionUsageLimit> usageLimits) 
        : base(actionOwner, name, actionDurationType, range, targetingType, damageType, damageDicePool, usageLimits)
    {
        SuccessRollAction = new SuccessRollAction(actionOwner, Name, actionDurationType, ERollType.Attack, []);
    }

    private SuccessRollAction SuccessRollAction { get; }

    public ERollType RollType => SuccessRollAction.RollType;

    public Task<ERollResult> GetPredeterminedResult()
    {
        return SuccessRollAction.GetPredeterminedResult();
    }

    public Task<ERollResult> GetResult(ERollResult defaultResult)
    {
        return SuccessRollAction.GetResult(defaultResult);
    }

    public Task<EAdvantage> GetRollAdvantage()
    {
        return SuccessRollAction.GetRollAdvantage();
    }

    public Task<DicePool> GetRollBonus()
    {
        return SuccessRollAction.GetRollBonus();
    }
}

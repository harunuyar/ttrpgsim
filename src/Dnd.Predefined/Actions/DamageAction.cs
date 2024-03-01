namespace Dnd.Predefined.Actions;

using Dnd._5eSRD.Models.DamageType;
using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class DamageAction : TargetingAction, IDamageAction
{
    public DamageAction(IGameActor actionOwner, string name, ActionDurationType actionDurationType, ActionRange range, TargetingType targetingType, DamageTypeModel damageType, DicePool damageDicePool)
        : base(actionOwner, name, actionDurationType, range, targetingType)
    {
        DamageType = damageType;
        AmountAction = new AmountAction(actionOwner, name, actionDurationType, damageDicePool);
    }

    public AmountAction AmountAction { get; }

    public DamageTypeModel DamageType { get; }

    public DicePool AmountDicePool => AmountAction.AmountDicePool;

    public Task<EAdvantage> GetAmountAdvantage()
    {
        return AmountAction.GetAmountAdvantage();
    }

    public Task<DicePool> GetAmountBonus()
    {
        return AmountAction.GetAmountBonus();
    }

    public Task<int> GetAmountResult(int defaultAmount)
    {
        return AmountAction.GetAmountResult(defaultAmount);
    }

    public Task<int?> GetPredeterminedAmount()
    {
        return AmountAction.GetPredeterminedAmount();
    }
}

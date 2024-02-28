namespace Dnd.Predefined.Actions;

using Dnd.Predefined.Commands.AmountBonusCommands;
using Dnd.Predefined.Commands.DamageBonusCommands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class AmountAction : Action, IAmountAction
{
    public AmountAction(IGameActor actionOwner, string name, ActionDurationType actionDurationType, DicePool amountDicePool)
        : base(actionOwner, name, actionDurationType)
    {
        AmountDicePool = amountDicePool;
    }

    public DicePool AmountDicePool { get; }

    public async Task<EAdvantage> GetAmountAdvantage()
    {
        var advantage = await new GetAmountAdvantage(ActionOwner, this, null).Execute();

        if (!advantage.IsSuccess)
        {
            throw new InvalidOperationException("GetAmountAdvantage: " + advantage.ErrorMessage);
        }

        return advantage.Values.Select(x => x.Item2).DefaultIfEmpty(EAdvantage.None).Aggregate((a, b) => a | b);
    }

    public async Task<DicePool> GetAmountBonus()
    {
        var modifiers = await new GetAmountModifiers(ActionOwner, this, null).Execute();

        if (!modifiers.IsSuccess)
        {
            throw new InvalidOperationException("GetAmountModifiers: " + modifiers.ErrorMessage);
        }

        var diceBonus = await new GetAmountDiceRollBonus(ActionOwner, this, null).Execute();

        if (!diceBonus.IsSuccess)
        {
            throw new InvalidOperationException("GetAmountDiceRollBonus: " + diceBonus.ErrorMessage);
        }

        return new DicePool(diceBonus.Values.Select(x => x.Item2), modifiers.Values.Select(x => x.Item2).DefaultIfEmpty(0).Sum());
    }

    public async Task<int> GetAmountResult(int defaultAmount)
    {
        var result = await new GetAmountResult(ActionOwner, this, null, defaultAmount).Execute();

        if (!result.IsSuccess)
        {
            throw new InvalidOperationException("GetAmountResult: " + result.ErrorMessage);
        }

        return result.Value;
    }

    public async Task<int?> GetPredeterminedAmount()
    {
        var result = await new GetPredefinedAmount(ActionOwner, this, null).Execute();

        if (!result.IsSuccess)
        {
            throw new InvalidOperationException("GetPredeterminedAmount: " + result.ErrorMessage);
        }

        return result.Value;
    }
}

namespace Dnd.Predefined.Actions;

using Dnd.Predefined.Commands.RollBonusCommands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class ARollAction : AAction, IRollAction
{
    public ARollAction(IGameActor actionOwner, string name, ActionDurationType actionDurationType, ERollType rollType) 
        : base(actionOwner, name, actionDurationType)
    {
        RollType = rollType;
    }

    public ERollType RollType { get; }

    public async Task<EAdvantage> GetRollAdvantage()
    {
        var advantage = await new GetAdvantage(ActionOwner, this, null).Execute();

        if (!advantage.IsSuccess)
        {
            throw new InvalidOperationException("GetAdvantage: " + advantage.ErrorMessage);
        }

        return advantage.Values.Select(x => x.Item2).DefaultIfEmpty(EAdvantage.None).Aggregate((a, b) => a | b);
    }

    public async Task<DicePool> GetRollBonus()
    {
        var modifiers = await new GetModifiers(ActionOwner, this, null).Execute();

        if (!modifiers.IsSuccess)
        {
            throw new InvalidOperationException("GetModifiers: " + modifiers.ErrorMessage);
        }

        var diceBonus = await new GetDiceRollBonus(ActionOwner, this, null).Execute();

        if (!diceBonus.IsSuccess)
        {
            throw new InvalidOperationException("GetDiceRollBonus: " + diceBonus.ErrorMessage);
        }

        return new DicePool(diceBonus.Values.Select(x => x.Item2), modifiers.Values.Select(x => x.Item2).DefaultIfEmpty(0).Sum());
    }
}

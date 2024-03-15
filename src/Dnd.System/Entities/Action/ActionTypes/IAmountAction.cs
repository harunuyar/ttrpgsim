namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.Entities.Action;
using Dnd.System.GameManagers.Dice;

public interface IAmountAction : IAction
{
    DicePool AmountDicePool { get; }
    EAmountRollType AmountRollType { get; }
}

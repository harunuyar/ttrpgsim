namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.GameManagers.Dice;

public interface IHealAction : IAction
{
    DicePool? HealDicePool { get; }
}

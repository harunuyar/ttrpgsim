namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.GameManagers.Dice;

public interface IRollAction : IAction
{
    ERollType RollType { get; }
    Task<DicePool> GetRollBonus();
    Task<EAdvantage> GetRollAdvantage();
}

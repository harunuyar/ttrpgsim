namespace Dnd.System.Entities.Action.ActionTypes;

using Dnd.System.GameManagers.Dice;

public interface ISuccessRollAction : IRollAction
{
    Task<ERollResult> GetPredeterminedResult();
    Task<ERollResult> GetResult(ERollResult defaultResult);
}

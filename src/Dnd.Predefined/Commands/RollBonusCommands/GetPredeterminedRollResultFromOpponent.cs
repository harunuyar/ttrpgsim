namespace Dnd.Predefined.Commands.RollBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetPredeterminedRollResultFromOpponent : ListCommand<ERollResult>
{
    internal GetPredeterminedRollResultFromOpponent(IGameActor actor, ISuccessRollAction action, IGameActor? opponent) : base(actor)
    {
        Action = action;
        Opponent = opponent;
    }

    public ISuccessRollAction Action { get; }

    public IGameActor? Opponent { get; }
}

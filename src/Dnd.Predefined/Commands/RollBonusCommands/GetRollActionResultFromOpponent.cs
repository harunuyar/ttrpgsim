namespace Dnd.Predefined.Commands.RollBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetRollActionResultFromOpponent : ListCommand<ERollResult>
{
    public GetRollActionResultFromOpponent(IGameActor actor, ISuccessRollAction action, IGameActor opponent, ERollResult defaultRollResult) : base(actor)
    {
        Action = action;
        Opponent = opponent;
        DefaultRollResult = defaultRollResult;
    }

    public ISuccessRollAction Action { get; }

    public IGameActor? Opponent { get; }

    public ERollResult DefaultRollResult { get; }
}

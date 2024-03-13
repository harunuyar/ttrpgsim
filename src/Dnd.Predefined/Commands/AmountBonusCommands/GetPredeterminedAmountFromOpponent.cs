namespace Dnd.Predefined.Commands.AmountBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class GetPredeterminedAmountFromOpponent : ValueCommand<int?>
{
    public GetPredeterminedAmountFromOpponent(IGameActor actor, IAmountAction amountAction, IGameActor? opponent) : base(actor)
    {
        AmountAction = amountAction;
        Opponent = opponent;
    }

    public IAmountAction AmountAction { get; }

    public IGameActor? Opponent { get; }
}

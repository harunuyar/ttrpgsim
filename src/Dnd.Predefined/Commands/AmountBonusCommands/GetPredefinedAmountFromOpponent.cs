namespace Dnd.Predefined.Commands.AmountBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class GetPredefinedAmountFromOpponent : ValueCommand<int?>
{
    public GetPredefinedAmountFromOpponent(IGameActor actor, IAmountAction amountAction, IGameActor opponent) : base(actor)
    {
        AmountAction = amountAction;
        Opponent = opponent;
    }

    public IAmountAction AmountAction { get; }

    public IGameActor? Opponent { get; }
}

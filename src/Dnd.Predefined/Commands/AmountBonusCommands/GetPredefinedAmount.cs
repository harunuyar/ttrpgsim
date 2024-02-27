namespace Dnd.Predefined.Commands.AmountBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class GetPredefinedAmount : ValueCommand<int?>
{
    public GetPredefinedAmount(IGameActor actor, IAmountAction amountAction, IGameActor? opponent) : base(actor)
    {
        AmountAction = amountAction;
        Opponent = opponent;
    }

    public IAmountAction AmountAction { get; }

    public IGameActor? Opponent { get; }

    protected override async Task InitializeResult()
    {
        if (Opponent is not null)
        {
            var fromOpponent = await new GetPredefinedAmountFromOpponent(Opponent, AmountAction, Actor).Execute();

            if (!fromOpponent.IsSuccess)
            {
                SetError("GetPredefinedAmountFromOpponent: " + fromOpponent.ErrorMessage);
                return;
            }

            Set(fromOpponent);
        }
    }
}

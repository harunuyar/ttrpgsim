namespace Dnd.Predefined.Commands.AmountBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class GetAmountResultFromOpponent : ScoreCommand
{
    public GetAmountResultFromOpponent(IGameActor actor, IAmountAction amountAction, IGameActor opponent, int defaultAmount) : base(actor)
    {
        AmountAction = amountAction;
        Opponent = opponent;
        DefaultAmount = defaultAmount;
    }

    public IAmountAction AmountAction { get; }

    public IGameActor Opponent { get; }

    public int DefaultAmount { get; }

    protected override Task InitializeResult()
    {
        SetBaseValue(DefaultAmount, "Default");

        return Task.CompletedTask;
    }
}

namespace Dnd.Predefined.Commands.AmountBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class GetAmountResult : ScoreCommand
{
    public GetAmountResult(IGameActor actor, IAmountAction amountAction, IGameActor? opponent, int defaultAmount) : base(actor)
    {
        AmountAction = amountAction;
        Opponent = opponent;
        DefaultAmount = defaultAmount;
    }

    public IAmountAction AmountAction { get; }

    public IGameActor? Opponent { get; }

    public int DefaultAmount { get; }

    protected override async Task InitializeResult()
    {
        SetBaseValue(DefaultAmount, "Default");

        await AmountAction.HandleUsageCommand(this);
    }

    protected override async Task FinalizeResult()
    {
        if (Opponent is not null)
        {
            var fromOpponent = await new GetAmountResultFromOpponent(Opponent, AmountAction, Actor, DefaultAmount).Execute();

            if (!fromOpponent.IsSuccess)
            {
                SetError("GetAmountResultFromOpponent: " + fromOpponent.ErrorMessage);
            }

            AddBonus(fromOpponent.Bonus);
        }
    }
}

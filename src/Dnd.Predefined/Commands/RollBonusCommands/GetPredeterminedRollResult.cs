namespace Dnd.Predefined.Commands.RollBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetPredeterminedRollResult : ListCommand<ERollResult>
{
    public GetPredeterminedRollResult(IGameActor actor, ISuccessRollAction action, IGameActor? opponent) : base(actor)
    {
        Action = action;
        Opponent = opponent;
    }

    public ISuccessRollAction Action { get; }

    public IGameActor? Opponent { get; }

    protected override async Task InitializeResult()
    {
        await Action.HandleUsageCommand(this);

        if (Opponent is not null)
        {
            var against = await new GetPredeterminedRollResultFromOpponent(Opponent, Action, Actor).Execute();

            if (!against.IsSuccess)
            {
                SetError("GetPredeterminedRollResultFromOpponent: " + against.ErrorMessage);
                return;
            }

            Set(against);
        }
    }
}

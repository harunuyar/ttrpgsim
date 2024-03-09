namespace Dnd.Predefined.Commands.RollBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetRollActionResult : ListCommand<ERollResult>
{
    public GetRollActionResult(IGameActor actor, ISuccessRollAction action, IGameActor? opponent, int rawDiceResult, ERollResult defaultRollResult) : base(actor)
    {
        Action = action;
        Opponent = opponent;
        RawDiceResult = rawDiceResult;
        DefaultRollResult = defaultRollResult;
    }

    public ISuccessRollAction Action { get; }

    public IGameActor? Opponent { get; }

    public int RawDiceResult { get; }

    public ERollResult DefaultRollResult { get; }

    protected override async Task InitializeResult()
    {
        await Action.HandleUsageCommand(this);

        if (Opponent is not null)
        {
            var against = await new GetRollActionResultFromOpponent(Opponent, Action, Actor, DefaultRollResult).Execute();

            if (!against.IsSuccess)
            {
                SetError("GetActionResultFromOpponent: " + against.ErrorMessage);
                return;
            }

            Set(against);
        }
    }
}

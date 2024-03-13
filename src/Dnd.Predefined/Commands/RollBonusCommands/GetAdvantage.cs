namespace Dnd.Predefined.Commands.RollBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetAdvantage : ListCommand<EAdvantage>
{
    public GetAdvantage(IGameActor actor, ISuccessRollAction action, IGameActor? opponent) : base(actor)
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
            var advantageAgainst = await new GetAdvantageFromOpponent(Opponent, Action, Actor).Execute();

            if (!advantageAgainst.IsSuccess)
            {
                SetError("GetAdvantageFromOpponent: " + advantageAgainst.ErrorMessage);
                return;
            }

            Set(advantageAgainst);
        }
    }
}

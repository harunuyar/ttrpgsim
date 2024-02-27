namespace Dnd.Predefined.Commands.RollBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetDiceRollBonus : ListCommand<DiceRoll>
{
    public GetDiceRollBonus(IGameActor actor, IRollAction action, IGameActor? opponent) : base(actor)
    {
        Action = action;
        Opponent = opponent;
    }

    public IRollAction Action { get; }

    public IGameActor? Opponent { get; }

    protected override async Task InitializeResult()
    {
        await Action.HandleUsageCommand(this);

        if (Opponent is not null)
        {
            var against = await new GetDiceRollBonusFromOpponent(Opponent, Action, Actor).Execute();

            if (!against.IsSuccess)
            {
                SetError("GetDiceRollBonusFromOpponent: " + against.ErrorMessage);
                return;
            }

            Set(against);
        }
    }
}

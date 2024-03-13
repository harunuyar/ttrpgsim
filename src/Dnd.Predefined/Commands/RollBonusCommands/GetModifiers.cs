namespace Dnd.Predefined.Commands.RollBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class GetModifiers : ListCommand<DicePool>
{
    public GetModifiers(IGameActor actor, ISuccessRollAction action, IGameActor? opponent) : base(actor)
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
            var against = await new GetModifiersFromOpponent(Opponent, Action, Actor).Execute();

            if (!against.IsSuccess)
            {
                SetError("GetModifiersFromOpponent: " + against.ErrorMessage);
                return;
            }

            Set(against);
        }
    }

    public void AddValue(int value, string message)
    {
        AddValue(DicePool.OfConstant(value), message);
    }
}

namespace Dnd.Predefined.Commands.RollBonusCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class GetModifiers : ListCommand<int>
{
    public GetModifiers(IGameActor actor, IRollAction action, IGameActor? opponent) : base(actor)
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
            var against = await new GetModifiersFromOpponent(Opponent, Action, Actor).Execute();

            if (!against.IsSuccess)
            {
                SetError("GetModifiersFromOpponent: " + against.ErrorMessage);
                return;
            }

            Set(against);
        }
    }
}

namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class IsActionAvailable : ValueCommand<bool>
{
    public IsActionAvailable(IGameActor actor, IAction action, IGameActor? opponent) : base(actor)
    {
        Action = action;
        Opponent = opponent;
    }

    public IAction Action { get; }

    public IGameActor? Opponent { get; }

    protected override async Task InitializeResult()
    {
        await Action.HandleUsageCommand(this);

        if (Opponent is not null)
        {
            var fromOpponent = await new IsActionAvailableFromOpponent(Opponent, Action, Actor).Execute();

            if (!fromOpponent.IsSuccess)
            {
                SetError("IsActionAvailableFromOpponent: " + fromOpponent.ErrorMessage);
                return;
            }

            Set(fromOpponent);
        }
    }
}

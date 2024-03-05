namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class IsActionAvailableFromOpponent : ValueCommand<bool>
{
    public IsActionAvailableFromOpponent(IGameActor actor, IAction action, IGameActor opponent) : base(actor)
    {
        Action = action;
        Opponent = opponent;
    }

    public IAction Action { get; }

    public IGameActor Opponent { get; }

    protected override Task InitializeResult()
    {
        SetValue(true, "Default");
        return Task.CompletedTask;
    }
}

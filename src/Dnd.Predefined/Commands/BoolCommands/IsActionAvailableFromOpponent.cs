namespace Dnd.Predefined.Commands.BoolCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;

public class IsActionAvailableFromOpponent : ValueCommand<bool>
{
    public IsActionAvailableFromOpponent(IGameActor actor, IEventAction action, IGameActor opponent) : base(actor)
    {
        Action = action;
        Opponent = opponent;
    }

    public IEventAction Action { get; }

    public IGameActor Opponent { get; }

    protected override Task InitializeResult()
    {
        SetValue(true, "Default");
        return Task.CompletedTask;
    }
}

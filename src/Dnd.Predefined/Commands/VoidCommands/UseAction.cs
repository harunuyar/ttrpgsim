namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action;
using Dnd.System.Entities.GameActor;

public class UseAction : VoidCommand
{
    public UseAction(IGameActor actor, IEventAction action) : base(actor)
    {
        Action = action;
    }

    public IEventAction Action { get; }

    protected override Task InitializeResult()
    {
        Action.MarkUse();
        return Task.CompletedTask;
    }
}

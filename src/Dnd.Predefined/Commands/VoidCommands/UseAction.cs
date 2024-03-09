namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.GameActor;

public class UseAction : VoidCommand
{
    public UseAction(IGameActor actor, IAction action) : base(actor)
    {
        Action = action;
    }

    public IAction Action { get; }

    protected override Task InitializeResult()
    {
        Action.MarkUse();
        return Task.CompletedTask;
    }
}

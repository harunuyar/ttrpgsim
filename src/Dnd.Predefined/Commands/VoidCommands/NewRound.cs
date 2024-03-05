namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class NewRound : VoidCommand
{
    public NewRound(IGameActor actor) : base(actor)
    {
    }

    protected override Task FinalizeResult()
    {
        SetMessage("A new round has begun");
        return Task.CompletedTask;
    }
}

namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class NewCombat : VoidCommand
{
    public NewCombat(IGameActor actor) : base(actor)
    {
    }

    protected override Task FinalizeResult()
    {
        SetMessage("A new combat has begun");
        return Task.CompletedTask;
    }
}

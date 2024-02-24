namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class EndTurn : VoidCommand
{
    public EndTurn(IGameActor character) : base(character)
    {
    }

    protected override Task FinalizeResult()
    {
        SetMessage($"{Actor.Name} has ended their turn.");

        return Task.CompletedTask;
    }
}

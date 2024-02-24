namespace Dnd.Predefined.Commands.VoidCommands;

using Dnd.System.CommandSystem.Commands;
using Dnd.System.Entities.GameActor;

public class TakeTurn : VoidCommand
{
    public TakeTurn(IGameActor character) : base(character)
    {
    }

    protected override Task FinalizeResult()
    {
        SetMessage($"It is {Actor.Name}'s turn");

        return Task.CompletedTask;
    }
}

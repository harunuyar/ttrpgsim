namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.GameActors;

public class TakeTurn : DndEventCommand
{
    public TakeTurn(IEventListener eventListener, IGameActor character) : base(eventListener, character)
    {
    }

    protected override void FinalizeResult()
    {
        Result.SetMessage($"It is {Actor.Name}'s turn");
    }
}

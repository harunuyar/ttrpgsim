namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.GameActors;

public class EndTurn : DndEventCommand
{
    public EndTurn(IEventListener eventListener, IGameActor character) : base(eventListener, character)
    {
    }

    protected override void FinalizeResult()
    {
        Result.SetMessage($"{Actor.Name} has ended their turn.");
    }
}

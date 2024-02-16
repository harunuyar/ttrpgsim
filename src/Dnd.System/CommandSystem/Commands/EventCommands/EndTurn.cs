namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.CommandSystem.Commands.BaseCommands;
using Dnd.System.Entities.GameActors;
using Dnd.System.Events.EventListener;

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

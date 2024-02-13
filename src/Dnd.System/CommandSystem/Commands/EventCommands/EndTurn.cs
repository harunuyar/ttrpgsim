namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.GameActors;

public class EndTurn : DndEventCommand
{
    public EndTurn(IEventListener eventListener, IGameActor character) : base(eventListener, character)
    {
    }

    protected override void InitializeEvent()
    {
    }

    protected override void FinalizeEvent()
    {
        EventResult.SetMessage($"{Character.Name} has ended their turn");
    }
}

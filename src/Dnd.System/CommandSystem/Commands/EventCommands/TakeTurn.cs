namespace Dnd.System.CommandSystem.Commands.EventCommands;

using Dnd.System.Entities.GameActors;

public class TakeTurn : DndEventCommand
{
    public TakeTurn(IEventListener eventListener, IGameActor character) : base(eventListener, character)
    {
    }

    protected override void InitializeEvent()
    {
    }

    protected override void FinalizeEvent()
    {
        EventResult.SetMessage($"It is {Character.Name}'s turn");
    }
}

namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActors;

public abstract class DndEventCommand : ADndCommand<EventResult>
{
    public DndEventCommand(IEventListener eventListener, IGameActor character) : base(character)
    {
        EventListener = eventListener;
        Result = EventResult.Success();
    }

    public override EventResult Result { get; }

    public IEventListener EventListener { get; }

    protected override void FinalAction()
    {
        EventListener.OnEventResult(Result);
    }
}

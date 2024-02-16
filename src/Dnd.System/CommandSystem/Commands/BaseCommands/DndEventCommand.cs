namespace Dnd.System.CommandSystem.Commands.BaseCommands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.GameActors;
using Dnd.System.Events.EventListener;

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

    public void SetMessage(string message)
    {
        Result.SetMessage(message);
    }

    public void SetMessageAndReturn(string message)
    {
        SetMessage(message);
        ForceComplete();
    }
}

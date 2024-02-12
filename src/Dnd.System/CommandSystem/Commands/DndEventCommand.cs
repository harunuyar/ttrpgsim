namespace Dnd.System.CommandSystem.Commands;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Characters;

public abstract class DndEventCommand : ICommand
{
    public DndEventCommand(IEventListener eventListener, ICharacter character)
    {
        EventListener = eventListener;
        Character = character;
        EventResult = EventResult.Success(this);
        ShouldVisitEntities = true;
    }

    public ICharacter Character { get; }

    public EventResult EventResult { get; }

    public IEventListener EventListener { get; }

    protected bool ShouldVisitEntities { get; set; }

    public EventResult Execute()
    {
        if (ShouldVisitEntities)
        {
            Character.HandleCommand(this);
        }

        if (EventResult.IsSuccess)
        {
            FinalizeEvent();
        }

        EventListener.OnEventResult(EventResult);
       
        return EventResult;
    }

    public abstract void FinalizeEvent();

    ICommandResult ICommand.Execute()
    {
        return Execute();
    }
}

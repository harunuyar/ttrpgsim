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

    protected EventResult EventResult { get; }

    public IEventListener EventListener { get; }

    protected bool ShouldVisitEntities { get; set; }

    public bool IsForceCompleted { get; private set; }

    public EventResult Execute()
    {
        EventResult.Reset();

        InitializeEvent();

        if (!IsForceCompleted && ShouldVisitEntities && EventResult.IsSuccess)
        {
            Character.HandleCommand(this);
        }

        if (!IsForceCompleted)
        {
            FinalizeEvent();
        }
        
        EventListener.OnEventResult(EventResult);
       
        return EventResult;
    }

    protected abstract void InitializeEvent();

    protected abstract void FinalizeEvent();

    ICommandResult ICommand.Execute()
    {
        return Execute();
    }

    public void ForceComplete()
    {
        IsForceCompleted = true;
    }

    public void SetEventMessageAndReturn(string message)
    {
        if (!IsForceCompleted)
        {
            EventResult.SetMessage(message);
            ForceComplete();
        }
    }

    public void SetErrorAndReturn(string message)
    {
        if (!IsForceCompleted)
        {
            EventResult.SetError(message);
            ForceComplete();
        }
    }
}

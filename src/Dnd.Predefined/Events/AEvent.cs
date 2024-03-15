namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class AEvent : IEvent
{
    public AEvent(string name, IGameActor eventOwner)
    {
        EventName = name;
        EventOwner = eventOwner;
        EventPhase = EEventPhase.New;
    }

    public EEventPhase EventPhase { get; private set; }

    public IGameActor EventOwner { get; }

    public List<IEvent> SequenceEvents { get; } = [];

    public string EventName { get; }

    public List<Task> FinalActions { get; } = [];

    public virtual bool IsWaitingForUserInput { get; protected set; }

    public virtual Task InitializeEvent()
    {
        SetEventPhase(EEventPhase.Initialized);
        return Task.CompletedTask;
    }

    public virtual Task<IEnumerable<IEvent>> RunEvent()
    {
        if (EventPhase >= EEventPhase.DoneRunning)
        {
            throw new InvalidOperationException("Event is already done running");
        }

        if (EventPhase == EEventPhase.WaitingOtherEvent)
        {
            throw new InvalidOperationException("Event is waiting for other event");
        }

        if (IsWaitingForUserInput)
        {
            throw new InvalidOperationException("Event is waiting for user input");
        }

        return RunEventImpl();
    }

    public virtual Task<IEnumerable<IEvent>> RunEventImpl()
    {
        SetEventPhase(EEventPhase.DoneRunning);
        return Task.FromResult(Enumerable.Empty<IEvent>());
    }

    public virtual async Task<IEnumerable<IEvent>> FinalizeEvent()
    {
        SetEventPhase(EEventPhase.Finalized);

        foreach (var finalAction in FinalActions)
        {
            await finalAction;
        }

        return SequenceEvents;
    }

    public void AddSequenceEvent(IEvent sequenceEvent)
    {
        SequenceEvents.Add(sequenceEvent);
    }

    public void AddFinalAction(Task finalAction)
    {
        FinalActions.Add(finalAction);
    }

    public void SetEventPhase(EEventPhase eventPhase)
    {
        EventPhase = eventPhase;
    }
}

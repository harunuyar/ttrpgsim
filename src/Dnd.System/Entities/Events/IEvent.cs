namespace Dnd.System.Entities.Events;

using Dnd.System.Entities.GameActor;

public enum EEventPhase : byte
{
    New = 0,
    Initialized,
    WaitingOtherEvent,
    DoneRunning,
    Finalized
}

public interface IEvent
{
    string EventName { get; }
    bool IsWaitingForUserInput { get; }
    EEventPhase EventPhase { get; }
    IGameActor EventOwner { get; }
    Task InitializeEvent();
    Task<IEnumerable<IEvent>> RunEvent();
    Task<IEnumerable<IEvent>> FinalizeEvent();
    void AddSequenceEvent(IEvent sequenceEvent);
    void AddFinalAction(Task finalAction);
    void SetEventPhase(EEventPhase eventPhase);
}

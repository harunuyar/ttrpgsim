namespace Dnd.Predefined.Events;

using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public class SavingThrowAttackEvent : AEvent, ISavingThrowAttackEvent
{
    public SavingThrowAttackEvent(string name, IGameActor eventOwner, ISavingThrowAttackAction action, IEnumerable<IGameActor> targets) 
        : base(name, eventOwner)
    {
        SavingThrowAttackAction = action;
        Targets = new HashSet<IGameActor>(targets);
    }

    public ISavingThrowAttackAction SavingThrowAttackAction { get; }

    public HashSet<IGameActor> Targets { get; }

    public ScoreResult? SaveDc { get; private set; }

    public override async Task InitializeEvent()
    {
        SaveDc = await new GetSavingDC(EventOwner, SavingThrowAttackAction).Execute();

        if (!SaveDc.IsSuccess)
        {
            throw new InvalidOperationException($"Failed to get save DC from {EventOwner.Name}. " + SaveDc.ErrorMessage);
        }

        SetEventPhase(EEventPhase.Initialized);
    }

    public override Task<IEnumerable<IEvent>> RunEvent()
    {
        if (SaveDc is null)
        {
            throw new InvalidOperationException("SaveDC is not initialized");
        }

        if (Targets.Count == 0)
        {
            throw new InvalidOperationException("No targets for saving throw attack");
        }

        if (EventPhase == EEventPhase.WaitingOtherEvent)
        {
            throw new InvalidOperationException("Waiting for other events to finish before continuing on this event");
        }

        if (DamageRollEvent is null || SavingThrowEvents is null)
        {
            DamageRollEvent = new AmountRollEvent($"{EventName}: Damage Roll", EventOwner, SavingThrowAttackAction, null, false);
            SavingThrowEvents = Targets.Select(t => new SuccessRollEvent($"{EventName}: Saving Throw", t, SavingThrowAttackAction, SaveDc.Value, EventOwner)).ToList();

            DamageRollEvent.AddFinalAction(new Task(NotifySubTaskFinished));
            foreach (var savingThrowEvent in SavingThrowEvents)
            {
                savingThrowEvent.AddFinalAction(new Task(NotifySubTaskFinished));
            }

            SetEventPhase(EEventPhase.WaitingOtherEvent);
            return Task.FromResult(SavingThrowEvents.Concat<IEvent>([DamageRollEvent]));
        }

        SetEventPhase(EEventPhase.DoneRunning);
        return Task.FromResult<IEnumerable<IEvent>>(
            SavingThrowEvents.Select(e => new SavingThrowDamageEvent($"{EventName}: Damage", e.EventOwner, SavingThrowAttackAction, DamageRollEvent.AmountResult ?? 0, e.RollResult?.IsSuccess() ?? false, EventOwner)));
    }

    private void NotifySubTaskFinished()
    {
        if (DamageRollEvent?.EventPhase == EEventPhase.Finalized && (SavingThrowEvents?.All(e => e.EventPhase == EEventPhase.Finalized) ?? false))
        {
            SetEventPhase(EEventPhase.DoneRunning);
        }
        else
        {
            SetEventPhase(EEventPhase.WaitingOtherEvent);
        }
    }

    public IAmountRollEvent? DamageRollEvent { get; private set; }

    public IEnumerable<ISuccessRollEvent>? SavingThrowEvents { get; private set; }
}

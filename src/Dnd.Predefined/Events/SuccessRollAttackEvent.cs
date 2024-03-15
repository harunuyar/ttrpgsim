namespace Dnd.Predefined.Events;

using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;
using Dnd.System.GameManagers.Dice;

public abstract class SuccessRollAttackEvent : AEvent
{
    public SuccessRollAttackEvent(string name, IGameActor eventOwner, IGameActor? target)
        : base(name, eventOwner)
    {
        Target = target;
    }

    public override bool IsWaitingForUserInput => Target is null;

    public IGameActor? Target { get; set; }

    public abstract Task<IEvent?> GetAttackEvent(ERollResult rollResult, IGameActor target);

    public abstract Task<ISuccessRollEvent> GetSuccessRollEvent(int targetResult, IGameActor opponent);

    public abstract Task<ScoreResult> GetTargetResult(IGameActor opponent);

    public ISuccessRollEvent? SuccessRollEvent { get; private set; }

    public IEvent? AttackEvent { get; private set; }

    public ScoreResult? TargetResult { get; private set; }

    public override async Task<IEnumerable<IEvent>> RunEventImpl()
    {
        if (TargetResult is null)
        {
            TargetResult = await GetTargetResult(Target!);

            if (TargetResult is null || !TargetResult.IsSuccess)
            {
                throw new InvalidOperationException($"Failed to get target result");
            }
        }

        if (SuccessRollEvent is null)
        {
            SuccessRollEvent = await GetSuccessRollEvent(TargetResult.Value, Target!);

            SuccessRollEvent.AddFinalAction(new Task(() => { SetEventPhase(EEventPhase.Initialized); }));

            SetEventPhase(EEventPhase.WaitingOtherEvent);
            return [SuccessRollEvent];
        }

        if (AttackEvent is null)
        {

            var rollResult = SuccessRollEvent is null
                ? ERollResult.Success
                : SuccessRollEvent.RollResult ?? throw new InvalidOperationException("Success roll event doesn't have a roll result");

            AttackEvent = await GetAttackEvent(rollResult, Target!);

            if (AttackEvent is not null)
            {
                AttackEvent.AddFinalAction(new Task(() => { SetEventPhase(EEventPhase.DoneRunning); }));
                SetEventPhase(EEventPhase.WaitingOtherEvent);
                return [AttackEvent];
            }
        }

        SetEventPhase(EEventPhase.DoneRunning);
        return [];
    }
}

namespace Dnd.Predefined.Events;

using Dnd.Predefined.Commands.ScoreCommands;
using Dnd.System.CommandSystem.Results;
using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class AttackRollEvent : AttackEvent, IAttackRollEvent
{
    public AttackRollEvent(string name, IGameActor eventOwner, IAttackRollAction action, IGameActor? target) 
        : base(name, eventOwner, action, target)
    {
        AttackRollAction = action;
        Target = target;
    }

    public override bool IsWaitingForUserInput => Target is null;

    public IAttackRollAction AttackRollAction { get; }

    public ScoreResult? ArmorClass { get; private set; }

    public override async Task<IEnumerable<IEvent>> RunEvent()
    {
        if (Target is null)
        {
            throw new InvalidOperationException("Target is not initialized");
        }

        ArmorClass = await new GetArmorClass(Target).Execute();

        if (!ArmorClass.IsSuccess)
        {
            throw new InvalidOperationException($"Failed to get armor class for {Target.Name}");
        }

        if (ArmorClass is null)
        {
            throw new InvalidOperationException("ArmorClass is not initialized");
        }

        if (EventPhase == EEventPhase.WaitingOtherEvent)
        {
            throw new InvalidOperationException("Event is waiting for other event");
        }

        if (SuccessRollEvent is null)
        {
            SuccessRollEvent = new SuccessRollEvent($"{EventName}: Attack To {Target.Name}", EventOwner, AttackRollAction, ArmorClass.Value, Target);
            SuccessRollEvent.AddFinalAction(new Task(() => { SetEventPhase(EEventPhase.Initialized); }));

            SetEventPhase(EEventPhase.WaitingOtherEvent);
            return [SuccessRollEvent];
        }

        return await base.RunEvent();
    }

    public SuccessRollEvent? SuccessRollEvent { get; private set; }
}

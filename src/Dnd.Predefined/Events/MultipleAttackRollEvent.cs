namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class MultipleAttackRollEvent : AEvent, IMultipleAttackRollEvent
{
    public MultipleAttackRollEvent(string name, IGameActor eventOwner, IAttackRollAction action, IEnumerable<IGameActor> targets) 
        : base(name, eventOwner)
    {
        AttackRollAction = action;
        Targets = targets.ToList();
    }

    public IAttackRollAction AttackRollAction { get; }

    public List<IGameActor> Targets { get; }

    public override Task<IEnumerable<IEvent>> RunEvent()
    {
        if (Targets.Count() != AttackRollAction.TargetingType.TargetCount)
        {
            throw new InvalidOperationException(EventName + " requires " + AttackRollAction.TargetingType.TargetCount + " targets");
        }

        var attackRollEvents = Targets.Select(x => new AttackRollEvent(EventName, EventOwner, AttackRollAction, x)).ToList();

        SetEventPhase(EEventPhase.DoneRunning);
        return Task.FromResult<IEnumerable<IEvent>>(attackRollEvents);
    }
}

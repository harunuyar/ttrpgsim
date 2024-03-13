namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class MultipleAttackEvent : AEvent, IMultipleAttackEvent
{
    public MultipleAttackEvent(string name, IGameActor eventOwner, IAttackAction action, IEnumerable<IGameActor> targets) 
        : base(name, eventOwner)
    {
        AttackAction = action;
        Targets = targets.ToList();
    }

    public IAttackAction AttackAction { get; }

    public List<IGameActor> Targets { get; }

    public override Task<IEnumerable<IEvent>> RunEvent()
    {
        if (AttackAction.TargetingType.TargetCount.HasValue && Targets.Count() != AttackAction.TargetingType.TargetCount)
        {
            throw new InvalidOperationException(EventName + " requires " + AttackAction.TargetingType.TargetCount + " targets");
        }

        var attackEvents = Targets.Select(x => new AttackEvent(EventName, EventOwner, AttackAction, x)).ToList();

        SetEventPhase(EEventPhase.DoneRunning);
        return Task.FromResult<IEnumerable<IEvent>>(attackEvents);
    }
}

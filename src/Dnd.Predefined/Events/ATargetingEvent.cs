namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class ATargetingEvent : AEvent
{
    public ATargetingEvent(string name, IGameActor eventOwner, ITargetingAction targetingAction, IEnumerable<IGameActor> targets) : base(name, eventOwner)
    {
        TargetingAction = targetingAction;
        Targets = targets.ToList();
    }

    public override bool IsWaitingForUserInput => base.IsWaitingForUserInput || TargetingAction.TargetingType.IsSuitable(Targets);

    public ITargetingAction TargetingAction { get; }

    public List<IGameActor> Targets { get; private set; }

    private bool isDistinctDone = false;

    public override Task<IEnumerable<IEvent>> RunEvent()
    {
        if (!isDistinctDone && TargetingAction.TargetingType.UniqueTargets)
        {
            Targets = Targets.Distinct().ToList();
        }

        isDistinctDone = true;

        return base.RunEvent();
    }
}

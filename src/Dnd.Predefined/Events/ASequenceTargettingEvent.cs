namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public abstract class ASequenceTargettingEvent : ATargetingEvent
{
    public ASequenceTargettingEvent(string name, IGameActor eventOwner, ITargetingAction targetingAction, IEnumerable<IGameActor> targets) 
        : base(name, eventOwner, targetingAction, targets)
    {
    }

    public List<IEvent>? SubEvents { get; private set; }

    public override async Task<IEnumerable<IEvent>> RunEventImpl()
    {
        if (SubEvents is null)
        {
            SubEvents = [];
            foreach (var target in Targets)
            {
                var newSubEvents = await CreateSubEvents(Targets.First());
                SubEvents.AddRange(newSubEvents);
            }
            
            SubEvents.ForEach(x => x.AddFinalAction(new Task(NotifyEventFinalized)));
        }

        return await base.RunEventImpl();
    }

    public void NotifyEventFinalized()
    {
        if (SubEvents is null)
        {
            throw new InvalidOperationException("SubEvents is not initialized");
        }

        if (SubEvents.All(x => x.EventPhase == EEventPhase.Finalized))
        {
            SetEventPhase(EEventPhase.Finalized);
        }
    }

    public abstract Task<IEnumerable<IEvent>> CreateSubEvents(IGameActor target);
}

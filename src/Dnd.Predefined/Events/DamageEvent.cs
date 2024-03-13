namespace Dnd.Predefined.Events;

using Dnd.System.Entities.Action.ActionTypes;
using Dnd.System.Entities.Events;
using Dnd.System.Entities.GameActor;

public class DamageEvent : AEvent, IDamageEvent
{
    public DamageEvent(string name, IGameActor eventOwner, IDamageAction damageAction, int amount) 
        : base(name, eventOwner)
    {
        DamageAction = damageAction;
        Amount = amount;
    }

    public IDamageAction DamageAction { get; }

    public int Amount { get; set; }

    public override Task<IEnumerable<IEvent>> RunEvent()
    {
        EventOwner.HitPoints.Damage(Amount);
        SetEventPhase(EEventPhase.DoneRunning);
        return Task.FromResult(Enumerable.Empty<IEvent>());
    }
}
